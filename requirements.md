# SEBS Requirements

## Functional Requirements

| ID | Requirement |
|----|-------------|
| FR1 | Student can search/browse equipment by category and see current availability |
| FR2 | The system shall reject a booking where the due date is not strictly after the booking date, and shall accept any booking where the equipment is available and the due date is after the booking date |
| FR3 | System must prevent booking equipment that is fully reserved or marked damaged |
| FR4 | Student can cancel a booking while it is Active |
| FR5 | Staff can check in returned equipment, marking the booking Completed |
| FR6 | Staff can check in equipment as damaged, which sets the booking to Completed and the equipment's damaged flag to true, regardless of the equipment's prior damaged state, so that the item is excluded from future bookings per FR3 until repaired |
| FR7 | Staff can mark damaged equipment as repaired, returning it to available stock |
| FR8 | System can identify a booking as overdue if the current date is past the due date and the booking is still Active |
| FR9 | Manager can generate a report, scoped to the current semester's active data, showing: total bookings per equipment item, count of currently overdue bookings, and count of items currently marked damaged — pending a reporting/query layer being added to the solution |

## Non-Functional Requirements

| ID | Requirement |
|----|-------------|
| NFR1 | Usability: A first-time student user shall be able to complete a booking (search, select, confirm) in 3 or fewer screens, without needing to consult a help guide |
| NFR2 | Reliability: the system must never let AvailableQuantity go negative or allow two bookings to claim the same last unit |
| NFR3 | Data integrity: equipment availability counts must stay accurate after every reserve/release/cancel action |
| NFR4 | Maintainability: the domain layer should stay decoupled from the GUI so it can be extended with new modules |
| NFR5 | Security: only authenticated staff can perform check-in/check-out and damage marking actions |
| NFR6 | Performance: The system shall return equipment search/availability results within 2 seconds under normal prototype load (single user, local test environment) |
| NFR7 | Accessibility: The GUI shall meet basic accessibility standards (sufficient colour contrast, keyboard-navigable forms, readable font sizes) so a student with a visual or motor impairment can complete a booking without needing external assistance |

## Acceptance Criteria

### Booking Creation (FR2, FR3)

- **Given** equipment is available (available quantity > 0 and not damaged) and the due date is after the booking date,
  **when** a student creates a booking,
  **then** the booking is created with status Active and the equipment's available quantity decreases by 1.

- **Given** equipment is fully reserved or marked damaged,
  **when** a student attempts to book it,
  **then** the booking is rejected and the equipment's available quantity is unchanged.

- **Given** the due date is not after the booking date,
  **when** a student attempts to create the booking,
  **then** the booking is rejected before any equipment quantity changes.

### Booking Cancellation (FR4)

- **Given** a booking is Pending or Active,
  **when** the student cancels it,
  **then** the booking status becomes Cancelled and the equipment's available quantity increases by 1.

- **Given** a booking is already Completed or Cancelled,
  **when** cancellation is attempted,
  **then** the cancellation is rejected and no equipment quantity or status changes.

### Staff Check-In With Damage (FR6)

- **Given** a booking is Active,
  **when** staff checks in the equipment as damaged with a valid staff ID,
  **then** the booking status becomes Completed, the staff ID is recorded, and the equipment's damaged flag is set to true.

- **Given** a booking is not Active (already Completed or Cancelled),
  **when** staff attempts any check-in on it,
  **then** the check-in is rejected and the booking is unchanged.

- **Given** no staff ID is provided,
  **when** a check-in is attempted,
  **then** the check-in is rejected and the booking remains Active.

### Overdue Detection (FR8)

- **Given** a booking is Active and the current date is after the due date,
  **when** the system checks the booking,
  **then** it is reported as overdue.

- **Given** a booking is Active and the current date is on or before the due date,
  **when** the system checks the booking,
  **then** it is reported as not overdue.

- **Given** a booking is Completed or Cancelled,
  **when** the system checks the booking (regardless of date),
  **then** it is reported as not overdue.

  ## Quality Attributes

| Attribute | Why it matters for SEBS | Related Requirement(s) |
|---|---|---|
| Usability | Students use this occasionally, not daily: the booking flow needs to be self-explanatory with no training required. | NFR1 |
| Reliability | Booking and equipment status must be dependable: a double-booking or a stock count going wrong means a student shows up to no equipment. | NFR2 |
| Data Integrity | Equipment/booking state must stay consistent as a booking moves through Pending, Active, Completed, Cancelled | NFR3, FR3, FR6 |
| Maintainability | The Rec Centre may want more modules later (e.g. reporting, more sports codes). Keeping the domain layer separate from the GUI means new features don't require rewriting core booking logic. | NFR4 |
| Security | Only staff should be able to check equipment in/out or mark it damaged. A student shouldn't be able to complete their own booking. | NFR5 |
| Performance | Search/availability needs to feel responsive, especially at peak times like the start of semester. | NFR6 |
| Accessibility | The system is replacing a manual, in-person process. Worth keeping in mind for GUI design so students with disabilities aren't worse off than under the old process. | NFR7 |

**Not prioritised for this project:** Compatibility and portability (e.g. cross-platform/cross-browser support) are lower priority. This is a single-semester prototype for one Rec Centre, not a multi-platform product, so scope was kept to what's achievable.