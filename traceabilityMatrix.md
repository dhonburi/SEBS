## Equipment Browsing (FR1)

| ID | Priority | Test Case | Requirement | Given / When / Then |
| --- | --- | --- | --- | --- |
| TC01 | Medium | Get all equipment returns the seeded list | FR1 | Browsing is a core part of the app. There is no specific AC bullet for FR1, but the app cannot function without it |
| TC02 | Low | Get all bookings returns all created bookings | FR1 (adjacent) | Basic getter with no specific AC. Mainly supports other tests and queries rather than a direct user-facing rule |

## Booking Creation (FR2, FR3, NFR2, NFR3)

| ID | Priority | Test Case | Requirement | Given / When / Then |
| --- | --- | --- | --- | --- |
| TC03 | High | Valid booking creates an Active booking and reduces availability | FR2, NFR3 | Matches AC bullet 1 exactly. Available equipment with a valid due date creates an Active booking and reduces the quantity by 1 |
| TC04 | High | Due date on or before the booking date is rejected | FR2 | Matches AC bullet 3 exactly. A due date that is not after the booking date is rejected before the quantity changes |
| TC05 | Medium | Due date exactly 1 day after the booking date succeeds | FR2 | Boundary case for TC04 and TC03. It checks the same rule at the exact edge, even though it does not have its own AC bullet |
| TC06 | High | Fully reserved equipment is rejected | FR3, NFR2 | Matches AC bullet 2 exactly. Fully reserved equipment is rejected and the quantity stays unchanged |
| TC07 | High | Damaged equipment is rejected | FR3 | Matches AC bullet 2 exactly. Equipment marked as damaged is rejected and the quantity stays unchanged |
| TC08 | Medium | Availability does not go below zero when repeatedly trying to book the last unit | NFR2 | NFR2 states this directly. It is a Non-Functional Requirement rather than one of the Given/When/Then AC bullets |
| TC09 | Medium | Unknown student ID is rejected | FR2 (data validity) | Not covered by any AC wording, but the app should handle invalid IDs without crashing |
| TC10 | Medium | Unknown equipment ID is rejected | FR3 (data validity) | Same reasoning as TC09. The app should handle invalid equipment IDs without crashing |

## Booking Cancellation (FR4)

| ID | Priority | Test Case | Requirement | Given / When / Then |
| --- | --- | --- | --- | --- |
| TC11 | High | Cancelling an Active booking releases the unit | FR4 | Matches AC bullet 4 exactly. An Active booking becomes Cancelled and the quantity increases by 1 |
| TC12 | High | Cancelling a Completed booking is rejected | FR4 | Matches AC bullet 5 exactly. An already Completed or Cancelled booking is rejected and remains unchanged |
| TC13 | High | Cancelling an already-Cancelled booking is rejected | FR4 | Matches AC bullet 5 exactly. This checks the other part of the "Completed or Cancelled" condition |
| TC14 | Medium | Cancelling an unknown booking ID is rejected | FR4 (data validity) | Not covered by the AC wording, but necessary to handle invalid booking IDs safely |

## Staff Check-In (FR5, NFR5)

| ID | Priority | Test Case | Requirement | Given / When / Then |
| --- | --- | --- | --- | --- |
| TC15 | Medium | Valid staff check-in completes the booking | FR5 | Tests the main FR5 function. There is no specific AC bullet for the normal successful check-in, only for the damaged check-in case |
| TC16 | High | Invalid staff ID is rejected and the booking stays Active | FR5, NFR5 | Matches AC bullet 8 exactly. The wording says "a check-in", so it also applies to the normal FR5 check-in |
| TC17 | Medium | Check-in with an unknown booking ID is rejected | FR5 (data validity) | Not covered by the AC wording |
| TC18 | High | Check-in on a Completed booking is rejected | FR5 | Matches AC bullet 7 exactly. It applies to any check-in on a Completed or Cancelled booking |
| TC19 | High | Check-in on a Cancelled booking is rejected | FR5 | Matches AC bullet 7 exactly. This checks the other part of the "Completed or Cancelled" condition |

## Damage Reporting & Repair (FR6, FR7)

| ID | Priority | Test Case | Requirement | Given / When / Then |
| --- | --- | --- | --- | --- |
| TC20 | High | Damaged check-in completes the booking and marks the equipment as damaged | FR6 | Matches AC bullet 6 exactly. An Active booking with a damaged check-in and valid staff ID becomes Completed and the equipment is marked as damaged |
| TC21 | High | Damaged check-in with an invalid staff ID is rejected | FR6 | Matches AC bullet 8 and applies it to the damaged check-in path |
| TC22 | High | Damaged check-in on a non-Active booking is rejected | FR6 | Matches AC bullet 7 and applies it to the damaged check-in path |
| TC23 | Medium | Damaged check-in still succeeds when the equipment is already damaged | FR6 | FR6 explicitly says this should work regardless of the previous damaged state, even though it does not have its own AC bullet |
| TC24 | Medium | Marking equipment as repaired clears the damaged flag | FR7 | Tests the main FR7 function. There is no AC bullet specifically covering FR7 |
| TC25 | Medium | Marking unknown equipment as repaired is rejected | FR7 (data validity) | Not covered by the AC wording |
| TC26 | Medium | Marking equipment as repaired with an invalid staff ID is rejected | FR7, NFR5 | Relates to NFR5 for security, but is not covered by a specific Given/When/Then AC bullet |

## Overdue Detection (FR8)

| ID | Priority | Test Case | Requirement | Given / When / Then |
| --- | --- | --- | --- | --- |
| TC27 | High | Active booking past its due date is overdue | FR8 | Matches AC bullet 9 exactly |
| TC28 | High | Active booking before its due date is not overdue | FR8 | Matches AC bullet 10 exactly |
| TC29 | High | Active booking on its due date is not overdue | FR8 | Matches AC bullet 10 exactly. The wording says "on or before", so this boundary is directly included |
| TC30 | High | Completed booking is never overdue | FR8 | Matches AC bullet 11 exactly |
| TC31 | High | Cancelled booking is never overdue | FR8 | Matches AC bullet 11 exactly. This checks the other part of the "Completed or Cancelled" condition |