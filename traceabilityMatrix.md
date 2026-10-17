## Equipment Browsing (FR1)

| ID | Priority | Test Case | Requirement | Given (Starting State) | When (Action) | Then (Expected Result) |
|---|---|---|---|---|---|---|
| TC01 | Medium | Get all equipment returns the seeded list | FR1 | Equipment has been added to the system | All equipment is requested | Every added item is returned |
| TC02 | Low | Get all bookings returns all created bookings | FR1 (adjacent) | One or more bookings have been created | All bookings are requested | Every created booking is returned |
| TC37 | High | Search by category returns only that category | FR1 | Equipment exists across several categories | A student filters by one category | Only equipment in that category is returned |
| TC38 | Medium | Name search is a case-insensitive partial match | FR1 | Equipment with names containing "Racket" exists | A student searches "  RACKET " | All items whose name contains "racket" are returned, ignoring case and surrounding spaces |
| TC39 | Medium | Category and name filters combine | FR1 | Equipment exists across several categories | A student filters by category and searches by name | Only items matching both filters are returned |
| TC40 | High | No filters returns all equipment, including unavailable items, with availability visible | FR1 | One item is marked damaged | A student browses with no filter | Every item is returned, and the damaged item reports as not available |
| TC41 | Low | Search with no match returns an empty list | FR1 (edge case) | No equipment exists in the requested category | A student filters by that category | An empty list is returned |
| TC42 | Medium | Category list is distinct and sorted | FR1 | Several items share categories | The category list is requested | Each category appears once, in alphabetical order |

## Booking Creation (FR2, FR3, NFR2, NFR3)

| ID | Priority | Test Case | Requirement | Given (Starting State) | When (Action) | Then (Expected Result) |
|---|---|---|---|---|---|---|
| TC03 | High | Valid booking creates an Active booking and reduces availability | FR2, NFR3 | Equipment is available and the due date is after the booking date | A student creates a booking | The booking is created with Active status, available quantity decreases by 1, and the booking is stored in the booking collection |
| TC04 | High | Due date on or before the booking date is rejected | FR2 | A due date that is on or before the booking date | A student attempts to create a booking | The booking is rejected and available quantity is unchanged |
| TC05 | Medium | Due date exactly 1 day after the booking date succeeds | FR2 | A due date exactly one day after the booking date | A student creates a booking | The booking succeeds (confirms the minimum valid gap is accepted) |
| TC06 | High | Fully reserved equipment is rejected | FR3, NFR2 | Equipment has zero units available | A student attempts to book it | The booking is rejected and available quantity remains unchanged |
| TC07 | High | Damaged equipment is rejected | FR3 | Equipment is marked as damaged (regardless of quantity available) | A student attempts to book it | The booking is rejected |
| TC08 | Medium | Availability never goes below zero on repeated last-unit attempts | NFR2 | Equipment has exactly one unit available | Multiple booking attempts are made in sequence | Only the first succeeds; available quantity never drops below zero |
| TC09 | Medium | Unknown student ID is rejected | FR2 (data validity) | A student ID that does not exist in the system | A booking is attempted | The booking is rejected with a "student not found" message |
| TC10 | Medium | Unknown equipment ID is rejected | FR3 (data validity) | An equipment ID that does not exist in the system | A booking is attempted | The booking is rejected with an "equipment not found" message |

## Booking Cancellation (FR4)

| ID | Priority | Test Case | Requirement | Given (Starting State) | When (Action) | Then (Expected Result) |
|---|---|---|---|---|---|---|
| TC11 | High | Cancelling an Active booking releases the unit | FR4 | A booking has Active status | The student cancels it | Status changes to Cancelled and available quantity increases by 1 |
| TC12 | High | Cancelling a Completed booking is rejected | FR4 | A booking has Completed status | Cancellation is attempted | The request is rejected and the booking remains Completed |
| TC13 | High | Cancelling an already-Cancelled booking is rejected | FR4 | A booking has Cancelled status | Cancellation is attempted again | The request is rejected and the booking remains Cancelled |
| TC14 | Medium | Cancelling an unknown booking ID is rejected | FR4 (data validity) | A booking ID that does not exist | Cancellation is attempted | The request is rejected with a "booking not found" message |

## Staff Check-In (FR5, NFR5)

| ID | Priority | Test Case | Requirement | Given (Starting State) | When (Action) | Then (Expected Result) |
|---|---|---|---|---|---|---|
| TC15 | Medium | Valid staff check-in completes the booking | FR5 | A booking has Active status | Staff check in the equipment with a valid staff ID | The booking status changes to Completed |
| TC16 | High | Invalid staff ID is rejected and the booking stays Active | FR5, NFR5 | A staff ID that does not match any registered staff member | Check-in is attempted | The request is rejected and the booking remains Active |
| TC17 | Medium | Check-in with an unknown booking ID is rejected | FR5 (data validity) | A booking ID that does not exist | Check-in is attempted | The request is rejected with a "booking not found" message |
| TC18 | High | Check-in on a Completed booking is rejected | FR5 | A booking has Completed status | Check-in is attempted again | The request is rejected and the booking remains Completed |
| TC19 | High | Check-in on a Cancelled booking is rejected | FR5 | A booking has Cancelled status | Check-in is attempted | The request is rejected and the booking remains Cancelled |

## Staff Authentication (NFR5)

| ID | Priority | Test Case | Requirement | Given (Starting State) | When (Action) | Then (Expected Result) |
|---|---|---|---|---|---|---|
| TC43 | High | Valid staff ID and PIN authenticates | NFR5 | A staff member is registered with a PIN | They sign in with the correct staff ID and PIN | Sign-in succeeds |
| TC44 | High | Wrong PIN is rejected | NFR5 | A staff member is registered with a PIN | They sign in with an incorrect PIN | Sign-in is rejected with "Invalid staff ID or PIN." |
| TC45 | High | Unknown staff ID gives the same message as a wrong PIN | NFR5 | A staff ID that is not registered | Sign-in is attempted | Sign-in is rejected with the same message as a wrong PIN, so valid staff IDs cannot be discovered |
| TC46 | High | Blank or near-miss PIN is rejected | NFR5 | A staff member is registered with a PIN | Sign-in is attempted with a null, empty, whitespace or one-digit-too-long PIN | Sign-in is rejected |
| TC47 | High | PIN is stored as a salted hash, not plain text | NFR5 | Two staff members are created with the same PIN | Their stored credentials are compared | The stored hash is not the PIN, each has a different salt and hash, and both still verify with the correct PIN |
| TC48 | Medium | Invalid PIN format is rejected when creating staff | NFR5 (data validity) | A PIN that is too short, too long, non-numeric or empty | A staff member is created with it | Creation is rejected with "PIN must be 4-8 digits." |

*Note: StaffForm can only be opened through the new Staff Sign In screen, which calls the same `AuthenticateStaff()` method covered by TC43-48. The signed-in staff ID is then fixed for the session and used for every check-in, damage and repair action.*

## Damage Reporting & Repair (FR6, FR7)

| ID | Priority | Test Case | Requirement | Given (Starting State) | When (Action) | Then (Expected Result) |
|---|---|---|---|---|---|---|
| TC20 | High | Damaged check-in completes the booking and marks equipment as damaged | FR6 | A booking has Active status | Staff check in the equipment as damaged with a valid staff ID | The booking status changes to Completed and the equipment's damaged flag is set to true |
| TC21 | High | Damaged check-in with an invalid staff ID is rejected | FR6 | A staff ID that does not match any registered staff member | A damaged check-in is attempted | The request is rejected and the booking remains Active |
| TC22 | High | Damaged check-in on a non-Active booking is rejected | FR6 | A booking is not Active (Completed or Cancelled) | A damaged check-in is attempted | The request is rejected and the booking is unchanged |
| TC23 | Medium | Damaged check-in still succeeds when equipment is already damaged | FR6 | Equipment is already marked as damaged | A booking for it is checked in as damaged again | The check-in still succeeds and the damaged flag remains true |
| TC24 | Medium | Marking equipment as repaired clears the damaged flag | FR7 | Equipment is marked as damaged | Staff mark it as repaired | The damaged flag is cleared to false |
| TC25 | Medium | Marking unknown equipment as repaired is rejected | FR7 (data validity) | An equipment ID that does not exist | Mark-repaired is attempted | The request is rejected with an "equipment not found" message |
| TC26 | Medium | Marking equipment as repaired with an invalid staff ID is rejected | FR7, NFR5 | A staff ID that does not match any registered staff member | Mark-repaired is attempted | The request is rejected |

## Overdue Detection (FR8)

| ID | Priority | Test Case | Requirement | Given (Starting State) | When (Action) | Then (Expected Result) |
|---|---|---|---|---|---|---|
| TC27 | High | Active booking past its due date is overdue | FR8 | A booking has Active status and a due date before the current date | Overdue status is checked | The booking is reported as overdue |
| TC28 | High | Active booking before its due date is not overdue | FR8 | A booking has Active status and a due date after the current date | Overdue status is checked | The booking is reported as not overdue |
| TC29 | High | Active booking on its due date is not overdue | FR8 | A booking has Active status and a due date equal to the current date | Overdue status is checked | The booking is reported as not overdue |
| TC30 | High | Completed booking is never overdue | FR8 | A booking has Completed status and a due date before the current date | Overdue status is checked | The booking is reported as not overdue |
| TC31 | High | Cancelled booking is never overdue | FR8 | A booking has Cancelled status and a due date before the current date | Overdue status is checked | The booking is reported as not overdue |
*Note: FR8's new GUI entry point (the "View Overdue Bookings" button) calls the same `GetOverdueBookings()` method already covered by TC27-31, with no additional logic of its own. It isn't given separate unit test cases for this reason, its correctness is manually verified in Task 4/7's GUI testing instead.*

## Manager Reporting (FR9)

| ID | Priority | Test Case | Requirement | Given (Starting State) | When (Action) | Then (Expected Result) |
|---|---|---|---|---|---|---|
| TC32 | Medium | Report totals bookings correctly per equipment item | FR9 | Multiple bookings exist across different equipment items | A manager report is generated | BookingsPerEquipment (keyed by EquipmentId) contains the correct booking count for each equipment item |
| TC33 | Medium | Report reflects the current overdue count | FR9, FR8 | A mix of overdue and non-overdue bookings exist | A manager report is generated | OverdueBookingCount matches the number of bookings currently overdue |
| TC34 | Medium | Report reflects the current damaged equipment count | FR9, FR7 | Some equipment items are marked damaged and some are not | A manager report is generated | DamagedEquipmentCount matches the number of equipment items currently marked damaged |
| TC35 | Low | Report on an empty system returns zero/empty results | FR9 (edge case) | No bookings or equipment exist in the system | A manager report is generated | BookingsPerEquipment is empty and both counts are zero |
| TC36 | High | Report counts same-named equipment items separately (regression) | FR9 (defect fix) | Two different equipment items share the same name and both have bookings | A manager report is generated | BookingsPerEquipment has a separate entry per EquipmentId with the correct count for each; the items are not merged |

## Unique IDs (Data Integrity, NFR3)

| ID | Priority | Test Case | Requirement | Given (Starting State) | When (Action) | Then (Expected Result) |
|---|---|---|---|---|---|---|
| TC49 | High | Duplicate equipment ID is rejected | NFR3 (data validity) | Equipment E001 already exists | Another equipment item with ID E001 is added | The add is rejected with "Equipment ID E001 already exists." and the original item is kept |
| TC50 | High | Duplicate student ID is rejected | NFR3 (data validity) | Student S001 already exists | Another student with ID S001 is added | The add is rejected and bookings for S001 still belong to the original student |
| TC51 | High | Duplicate staff ID is rejected | NFR3, NFR5 | Staff ST001 already exists with a PIN | Another staff member with ID ST001 and a different PIN is added | The add is rejected; only the original PIN can sign in, so a duplicate record can't be used to take over a staff ID |
| TC52 | Low | Null records are rejected | NFR3 (data validity) | An empty system | A null equipment item, student or staff member is added | The add is rejected with ArgumentNullException |
