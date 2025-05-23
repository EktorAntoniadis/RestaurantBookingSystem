# RestaurantBookingSystem

Αυτή η εφαρμογή αποτελεί υλοποίηση στα πλαίσια της διπλωματικής μου εργασίας.

Για να μπορέσετε να τρέξετε την εφαρμογή σε περιβάλλον υλοποίησης, χρειάζεται να έχετε τα παρακάτω:

- Visual Studio Community 2022
- Wamp Server 

Όταν κάνετε clone το αποθετήριο:
- ενεργοποιήστε το WAMP Server και έχετε μόνο τις προεγκατεστημένες ρυθμίσεις
- ανοίξτε το Package Manager Console και τρέξτε την εντολή

```
Update-Database -Context RestaurantBookingSystemDbContext -Project RestaurantBookingSystem.Data -StartupProject RestaurantBookingSystem.App
```
για να δημιουργηθεί η βάση.

Έπειτα τρέξτε σε την εφαρμογή στο Visual Studio για να ξεκινήσει η αρχικοποίηση των δεδομένων.
Δοκιμάστε τις διάφορες λειτουργικότητες της εφαρμογής.