# Test Execution Results

## Automated Tests (Unit/Integration)

38 MSTest test cases (TC01-TC38) covering SEBS.Core and SEBS.Services. Full requirement mapping is in traceabilityMatrix.md. All 38 tests currently pass, run through Visual Studio Test Explorer and also through the CI workflow (.github/workflows/ci.yml).

## Manual System/Acceptance Tests

| ID | Type | Ties to | Steps | Expected Result | Actual Result | Pass/Fail |
|----|------|---------|-------|------------------|----------------|-----------|
| MT01 | Acceptance | AC (Booking Creation) | Open StudentForm, browse equipment, book an available item with a due date after the booking date | Booking appears with Active status, available quantity drops by 1 in the grid | Booking created as expected, quantity updated in grid | Pass |
| MT02 | Acceptance/Boundary | AC (Booking Creation) | Attempt a booking with due date equal to booking date | Booking is rejected, no quantity change | Rejected as expected, no change to quantity | Pass |
| MT03 | System | FR3 | Attempt to book equipment with 0 available quantity | Booking rejected, UI shows the rejection message | Rejected, message shown correctly | Pass |
| MT04 | Acceptance | AC (Cancellation) | Cancel an Active booking from StudentForm | Status becomes Cancelled, available quantity goes up by 1 | Status changed to Cancelled, quantity released | Pass |
| MT05 | Acceptance | AC (Staff Check-In) | Check in an Active booking from StaffForm with a valid staff ID | Status becomes Completed, quantity released | Status changed to Completed, quantity released | Pass |
| MT06 | Acceptance | AC (Staff Check-In) | Check in with an invalid or unknown staff ID | Rejected, booking stays Active | Rejected, booking remained Active | Pass |
| MT07 | Acceptance | AC (Damage) | Check in a booking as damaged | Booking Completed, equipment shows damaged in the Equipment grid | Booking Completed, equipment flagged as damaged in grid | Pass |
| MT08 | System | FR7 | Mark that damaged equipment as repaired from StaffForm | Damaged flag clears, item can be booked again | Damaged flag cleared, item available again | Pass |
| MT09 | System | FR8 | Click "View Overdue Bookings" with an overdue booking present | Overdue booking(s) listed with correct student, equipment, and due date | Pass, verified 23/09 | Pass |
| MT10 | System | FR9 | Click "Manager Report" | Correct overdue count, damaged count, and per-equipment breakdown shown | Pass, verified 23/09 | Pass |

## Test Execution Summary

| Metric | Count |
|--------|-------|
| Planned | 48 |
| Executed | 48 |
| Passed | 48 |
| Failed | 0 |
| Blocked | 0 |
| Not Run | 0 |

(48 = 38 automated MSTest cases + 10 manual system/acceptance tests)

## Regression Notes

All 38 automated tests were re-run after the FR8 and FR9 work (overdue bookings view and manager report, including the equipment grouping fix) and all still pass. No existing functionality broke as a result of adding these features. Manual testing above confirms the same features work correctly end to end through the actual GUI, not just at the service layer.