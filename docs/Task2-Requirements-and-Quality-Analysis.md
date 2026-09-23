# Task 2: Requirements and Quality Analysis

**Project:** SEBS — Student Equipment Booking System
**Course:** ENSE707 — Software Quality Assurance

SEBS allows students to book university equipment (e.g., sports/lab gear tracked by category and quantity), and allows staff to check equipment back in, record damage, and mark items as repaired. Bookings have a lifecycle (Pending → Active → Completed/Cancelled), a due date, and overdue detection.

---

## 1. Initial Functional Requirements

| ID | Requirement |
|----|-------------|
| FR-01 | The system shall maintain a catalogue of equipment items, each with a unique equipment ID, name, category, and total quantity. |
| FR-02 | The system shall record students with a unique student ID, name, and email address. |
| FR-03 | The system shall record staff members with a unique staff ID and name. |
| FR-04 | The system shall allow a student to create a booking for an available equipment item, specifying a booking date and a due date. |
| FR-05 | The system shall reject a booking if the equipment item has no available quantity or is marked as damaged. |
| FR-06 | The system shall reject a booking whose due date is not after the booking date. |
| FR-07 | When a booking is created, the system shall decrease the equipment's available quantity by one. |
| FR-08 | The system shall allow a student to cancel a booking that is in the Pending or Active state; cancellation shall return the reserved item to the available pool. |
| FR-09 | The system shall allow a staff member to complete (check in) an Active booking; completion shall return the item to the available pool and record the staff ID of the person who processed the check-in. |
| FR-10 | The system shall allow a staff member to complete a booking with a damage report, which marks the equipment as damaged and makes it unavailable for further bookings. |
| FR-11 | The system shall allow a staff member to mark damaged equipment as repaired, making it available for booking again. |
| FR-12 | The system shall identify a booking as overdue when it is still Active and the current date is later than its due date. |
| FR-13 | The system shall prevent completion or cancellation of bookings that are already Completed or Cancelled. |
| FR-14 | The system shall ensure available quantity never exceeds total quantity and never falls below zero. |

## 2. Initial Non-Functional Requirements

| ID | Category | Requirement |
|----|----------|-------------|
| NFR-01 | Performance | Booking creation, cancellation, and check-in operations shall complete within 2 seconds under normal load. |
| NFR-02 | Reliability | The system shall never lose track of equipment stock: after any sequence of operations, the sum of items on loan and items available shall equal the total quantity. |
| NFR-03 | Availability | The system shall be available during library/service-desk operating hours (8am–9pm), with at least 99% uptime in that window. |
| NFR-04 | Security | Only authenticated staff members shall be able to complete check-ins, record damage, or mark equipment as repaired. |
| NFR-05 | Usability | A first-time student user shall be able to complete a booking without training or documentation. |
| NFR-06 | Maintainability | Core business logic (booking lifecycle, stock rules) shall be isolated in a separate library (SEBS.Core) with no UI or database dependencies, and covered by automated unit tests. |
| NFR-07 | Testability | All business rules shall be verifiable through automated tests without requiring a running UI or external services. |
| NFR-08 | Portability | The core library shall run on any platform supported by .NET (Windows, Linux, macOS). |
| NFR-09 | Data integrity | Invalid inputs (empty IDs, non-positive quantities, invalid date ranges) shall be rejected with a descriptive error and shall not alter system state. |
| NFR-10 | Accessibility | Any user-facing interface shall conform to WCAG 2.1 Level AA. |

## 3. Requirements Analysis

Each requirement is judged against six quality criteria: **clarity** (unambiguous), **completeness** (nothing essential missing), **consistency** (no contradictions), **correctness** (reflects real stakeholder need), **feasibility** (implementable with available resources), and **testability** (a test can objectively pass/fail it).

### 3.1 Analysis of selected requirements

**FR-04 (create booking)**
- *Clarity:* Weak — "available equipment item" is defined only implicitly; it does not say who supplies the booking date (user-entered vs. system clock) or whether there is a maximum loan period.
- *Completeness:* Incomplete — no limit on how many concurrent bookings one student may hold, and no rule for booking equipment in advance vs. immediately.
- *Consistency:* Consistent with FR-05/FR-07.
- *Correctness:* Matches the stakeholder need (students borrow equipment).
- *Feasibility:* High — already implemented in `Booking`'s constructor.
- *Testability:* Partially testable — "allow a student to create a booking" is testable, but the missing constraints (loan limits) cannot be tested because they are unstated.

**FR-08 (cancel booking)**
- *Clarity:* Good — the permitted states are named explicitly.
- *Consistency:* **Inconsistency found:** FR-08 says a *Pending* booking can be cancelled and cancellation "returns the reserved item", but under FR-07 a reservation is only taken when a booking is created as Active. The code (`Booking.Cancel` → `Equipment.Release`) releases stock even for Pending bookings, yet no code path ever creates a Pending booking. Either the Pending state should be removed, or the requirements must define when a booking is Pending and whether Pending reserves stock.
- *Testability:* Testable for the Active path; the Pending path is untestable as specified.

**FR-12 (overdue detection)**
- *Clarity:* Ambiguous — "current date is later than its due date" does not state granularity. Is a booking due at 2pm overdue at 2:01pm, or only the next day? The implementation (`IsOverdue`) compares full `DateTime` values, so a booking becomes overdue one second after the due instant — this should be stated explicitly.
- *Completeness:* Incomplete — no consequence of being overdue is specified (fine? block on new bookings? notification?).
- *Testability:* Testable once the granularity is pinned down.

**NFR-01 (performance)**
- *Clarity/Testability:* Weak as originally drafted in many student specs ("the system shall be fast"). The version above is better because it names operations and a bound (2 s), but "normal load" is still undefined — see the rewrite in Section 4.

**NFR-05 (usability)**
- *Testability:* Poor — "without training" has no measurable pass/fail condition. Who is the test subject? What counts as "complete a booking"? See rewrite in Section 4.

**NFR-04 (security)**
- *Correctness/Consistency:* **Gap between requirement and implementation:** the code only checks that *some* non-empty staff ID string is supplied (`Complete(staffId)`); it does not authenticate the staff member or verify the ID exists. The requirement is correct; the current implementation does not yet satisfy it. This is a useful finding for the quality report, not a reason to weaken the requirement.

### 3.2 Summary of defects found

| Defect | Type | Affected |
|--------|------|----------|
| Pending state is specified but no rule says how a booking becomes Pending or whether it reserves stock | Inconsistency / incompleteness | FR-07, FR-08, `BookingStatus` |
| No per-student loan limit or maximum loan duration defined | Incompleteness | FR-04 |
| Overdue granularity (date vs. exact time) unspecified | Ambiguity | FR-12 |
| "Normal load" and user counts undefined | Untestable | NFR-01 |
| "Without training" unmeasurable | Untestable | NFR-05 |
| Staff authentication required but not implemented (any non-empty string accepted) | Correctness gap | NFR-04, FR-09 |
| Damage marking applies to the whole equipment record, so one damaged unit makes all remaining units of that item unavailable (`Equipment.IsDamaged` is item-level, not unit-level) | Correctness gap | FR-10, FR-05 |

## 4. Rewritten Requirements (Improved Testability)

Each rewrite replaces vague language with measurable, verifiable conditions.

**FR-04 → FR-04a (rewritten)**
> When a student requests a booking for an equipment item, the system shall create the booking if and only if: (a) the item's available quantity is ≥ 1, (b) the item is not marked damaged, (c) the due date/time is strictly later than the booking date/time, and (d) the student currently holds fewer than 3 Active bookings. On success, the booking status shall be Active and the item's available quantity shall be reduced by exactly 1. On failure, the system shall raise a descriptive error and leave all quantities and statuses unchanged.

*Why it's better:* every condition is a boolean a unit test can assert; the post-conditions (state after success and after failure) are explicit.

**FR-12 → FR-12a (rewritten)**
> A booking shall be reported as overdue if and only if its status is Active and the evaluation timestamp is strictly later than the booking's due timestamp, compared to the second. Completed and Cancelled bookings shall never be reported as overdue, regardless of date.

**NFR-01 → NFR-01a (rewritten)**
> With a catalogue of 1,000 equipment items and 5,000 stored bookings, the 95th percentile response time for booking creation, cancellation, and check-in shall not exceed 2 seconds, measured over 100 consecutive operations on the reference deployment environment.

*Why it's better:* defines load (data volume), the statistic (p95), the sample size, and the environment — a load test can pass or fail it objectively.

**NFR-05 → NFR-05a (rewritten)**
> In moderated usability testing, at least 8 of 10 first-time student participants shall complete the task "book a named equipment item for a 3-day loan" in under 3 minutes without assistance, and the task shall achieve a System Usability Scale (SUS) score of at least 70.

**NFR-02 → NFR-02a (rewritten)**
> For every equipment item and after every operation (booking creation, cancellation, check-in, damage report, repair), the invariant `0 ≤ AvailableQuantity ≤ TotalQuantity` shall hold, and `AvailableQuantity + count(Active bookings for the item)` shall equal `TotalQuantity`. This invariant shall be enforced by automated tests covering every state-changing operation.

## 5. Acceptance Criteria for Key Features

Written in Given/When/Then form so each criterion maps directly to an automated test case (e.g., MSTest tests in SEBS.Tests).

### Feature 1: Create a booking

- **AC1.1** — Given an equipment item with available quantity 2 and no damage, when a student creates a booking with a due date after the booking date, then the booking status is Active, the available quantity becomes 1, and the booking records the student, equipment, and both dates.
- **AC1.2** — Given an equipment item with available quantity 0, when a student attempts to create a booking, then the system rejects it with "Equipment is not available for booking" and the quantity remains 0.
- **AC1.3** — Given an equipment item marked as damaged, when a student attempts to create a booking, then the booking is rejected and no quantity change occurs.
- **AC1.4** — Given any equipment item, when a booking is attempted with a due date equal to or earlier than the booking date, then the booking is rejected with a date-validation error and no reservation is made.
- **AC1.5** — Given any booking attempt, when the booking ID is empty or whitespace, then the booking is rejected with an ID-validation error.

### Feature 2: Cancel a booking

- **AC2.1** — Given an Active booking on an item whose available quantity is 1 (of 2), when the booking is cancelled, then the status becomes Cancelled and available quantity returns to 2.
- **AC2.2** — Given a Completed booking, when cancellation is attempted, then the system rejects it with "Only active or pending bookings can be cancelled" and the status remains Completed.
- **AC2.3** — Given a Cancelled booking, when cancellation is attempted again, then the second attempt is rejected and the available quantity is not incremented a second time.

### Feature 3: Check-in (complete a booking)

- **AC3.1** — Given an Active booking, when a staff member with a valid staff ID checks it in, then the status becomes Completed, the item's available quantity increases by 1, and the booking records that staff member's ID.
- **AC3.2** — Given an Active booking, when check-in is attempted with an empty or whitespace staff ID, then it is rejected with "Staff ID is required" and the booking remains Active.
- **AC3.3** — Given a Cancelled booking, when check-in is attempted, then it is rejected with "Only active bookings can be completed."

### Feature 4: Damage reporting and repair

- **AC4.1** — Given an Active booking, when a staff member checks it in with a damage report, then the booking becomes Completed and the equipment is marked damaged.
- **AC4.2** — Given a damaged equipment item with available quantity ≥ 1, when a student attempts to book it, then the booking is rejected.
- **AC4.3** — Given a damaged equipment item, when a staff member marks it repaired, then `IsDamaged` is false and the item can be booked again (subject to available quantity).

### Feature 5: Overdue detection

- **AC5.1** — Given an Active booking due on 2026-08-10, when overdue status is evaluated on 2026-08-11, then the booking is reported overdue.
- **AC5.2** — Given the same booking evaluated on 2026-08-09 (or exactly at the due instant), then it is not reported overdue.
- **AC5.3** — Given a Completed or Cancelled booking with a past due date, when overdue status is evaluated, then it is not reported overdue.

## 6. Relevant Software Quality Attributes

| Attribute | Relevance to SEBS | How it is (or should be) addressed |
|-----------|-------------------|-------------------------------------|
| **Reliability** | Critical. The system's core promise is accurate stock accounting; a lost or double-counted item means students face wrongly rejected bookings or the university loses equipment. | Stock invariant (NFR-02a) enforced in `Equipment.Reserve`/`Release` guards; state-transition guards in `Booking`; regression tests for every state change. |
| **Correctness / Functional suitability** | Booking lifecycle rules (only Active bookings can complete; only Active/Pending can cancel) must match policy exactly. | Guard clauses throwing `InvalidOperationException`; acceptance criteria in Section 5 traced to unit tests. |
| **Testability** | This is an SQA project — the design must support automated verification. | SEBS.Core is a pure domain library with no I/O; `IsOverdue(DateTime)` takes the clock as a parameter instead of reading `DateTime.Now`, so time-dependent behaviour is deterministic in tests. |
| **Maintainability** | Requirements will evolve (loan limits, fines, unit-level damage). Change should be localized. | Separation of SEBS.Core (domain) from SEBS.Tests; small single-responsibility classes; enum-based status rather than string flags. |
| **Security** | Staff-only operations (check-in, damage, repair) must not be executable by students; student personal data (name, email) must be protected. | Currently only string validation exists — authentication/authorization is a required future layer (see NFR-04 gap in Section 3.2). Personal data handling should follow the NZ Privacy Act 2020. |
| **Usability** | Users are students with no training; the booking flow must be self-evident or equipment desks revert to paper. | NFR-05a defines a measurable usability test (completion rate + SUS score). |
| **Performance efficiency** | Load is modest (campus-scale), but peak demand occurs at semester start and before events; slow responses cause desk queues. | NFR-01a sets a p95 bound under defined load; core operations are O(1) state changes, so risk is low. |
| **Accessibility** | A university system must serve students with disabilities; often a legal/policy obligation. | NFR-10: WCAG 2.1 AA conformance for any UI, verified with automated (axe) plus manual screen-reader checks. |
| **Portability** | The university may host on Windows or Linux; developers use varied machines. | SEBS.Core targets cross-platform .NET with no OS-specific dependencies (NFR-08). |
| **Compatibility** | A future web UI must work across the browsers students actually use, and the core library must integrate with a persistence layer without modification. | Keep the domain library dependency-free; browser support matrix to be defined with the UI. |

### Prioritisation

For SEBS the top three attributes are **reliability**, **correctness**, and **testability**: the system is fundamentally a small state machine over shared stock, so its value depends on that state machine never being wrong, and on being able to prove it isn't. Usability and security become dominant once the system is exposed to real students through a UI; performance and portability are lower risk at campus scale.
