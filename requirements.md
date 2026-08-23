# SEBS Requirements

## Functional Requirements

| ID | Requirement |
|----|-------------|
| FR1 | Student can search/browse equipment by category and see current availability |
| FR2 | Student can book an available item of equipment for a date range (booking date to due date) |
| FR3 | System must prevent booking equipment that is fully reserved or marked damaged |
| FR4 | Student can cancel a booking while it is Pending or Active |
| FR5 | Staff can check in returned equipment, marking the booking Completed |
| FR6 | Staff can check in equipment as damaged, marking both the booking Completed and the equipment as damaged |
| FR7 | Staff can mark damaged equipment as repaired, returning it to available stock |
| FR8 | System can identify a booking as overdue if the current date is past the due date and the booking is still Active |
| FR9 | Manager can generate a report of equipment usage (bookings per item, overdue count, damaged items) |

## Non-Functional Requirements

| ID | Requirement |
|----|-------------|
| NFR1 | Usability: the GUI should let a student complete a booking in a small number of steps without training |
| NFR2 | Reliability: the system must never let AvailableQuantity go negative or allow two bookings to claim the same last unit |
| NFR3 | Data integrity: equipment availability counts must stay accurate after every reserve/release/cancel action |
| NFR4 | Maintainability: the domain layer should stay decoupled from the GUI so it can be extended with new modules |
| NFR5 | Security: only authenticated staff can perform check-in/check-out and damage marking actions |
| NFR6 | Performance: search/availability results should return quickly enough to feel instant to the user (e.g. under 1-2 seconds for the prototype) |