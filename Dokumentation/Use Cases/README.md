# Use Case Overview

| Done? | UC ID  | Use Case                                            |
|-------|--------|-----------------------------------------------------|
| [x]   | UC-1.1 | Display of relevant data                            |
| [x]   | UC-1.2 | Upload of CSV files                                 |
| [x]   | UC-1.3 | Add/remove industries to lockdown/restrictions list |
| [x]   | UC-1.4 | Choose municipality                                 |
| [x]   | UC-1.5 | Lockdown/restriction indicator                      |
| [x]   | UC-1.6 | Display general restrictions                        |

<br><br>

# Use Cases

## Display of relevant data  - UC-1.1
<details>
<b>ID: </b> UC-1.1<br>
<b>Priority: </b> High <br>
<b>Actors: </b>Municipality worker <br>
<b>Description: </b> Dashboard for corona data. <br>
<b>Trigger:  </b> Display when municipality is selected.

---

<h4>Type: </h4>

-  [x] External 
-  [ ] Temporal 
---
<h4>Preconditions</h4>

1. Choose CSVs with up to date data.
---
<h4>Normal Course</h4>

1. Data is pulled when CSVs are chosen.
2. The relevant data is shown.
---
<h4>Alternative Courses</h4>

1. User has not chosen CSVs. 
2. Failed to load data.
---
<h4>Postconditions</h4>

1. Users can work with the data, see predictions and restrictions.

</details>

## Upload of CSV files - UC-1.2
<details>
<b>ID: </b> UC-1.2<br>
<b>Priority: </b> High <br>
<b>Actors: </b>Municipality worker <br>
<b>Description: </b> Ability to upload CSV files with corona data. <br>
<b>Trigger:  </b> When the user clicks upload data files.

---

<h4>Type: </h4>

-  [x] External 
-  [ ] Temporal 
---
<h4>Preconditions</h4>

1. The CSV upload page is shown correctly.
2. CSV files are downloaded and usable.

---
<h4>Normal Course</h4>

1. User chooses CSV upload to pull the data.
2. User picks CSVs in file explorer.

---
<h4>Alternative Courses</h4>

1. User has not chosen CSVs.
2. Failed to load data/locate the files.
3. CSV upload is unresponsive.

---
<h4>Postconditions</h4>

1. Files are successfully uploaded and the data can be used.

</details>

## Add/Remove industries to lockdown/restrictions list - UC-1.3
<details>
<b>ID: </b> UC-1.3<br>
<b>Priority: </b> High <br>
<b>Actors: </b>Municipality worker <br>
<b>Description: </b> Ability to Add/Remove industries to lockdown/restrictions list. <br>
<b>Trigger:  </b> The app has given a suggestion to add/remove industries from lockdown/restrictions list based on the latest corona data.

---

<h4>Type: </h4>

-  [x] External 
-  [ ] Temporal 
---
<h4>Preconditions</h4>

1. The user has picked their municipality.
2. The municipality worker has concluded, based on the data and suggestions given by the program, that an industry can either be locked down or restricted or be opened again.
3. CSV data is up to date.


---
<h4>Normal Course</h4>

1. User Adds/removes an industry code to/from the lockdown/restrictions list  from the list of all industries.
2. The user can view the lockdown/restrictions list and add/remove to it.
3. Insertion of data in database.


---
<h4>Alternative Courses</h4>

1. CSV data is outdated
2. Municipality is not chosen.
3. Error in database connection


---
<h4>Postconditions</h4>

1. The different industries have relevant lockdown/restriction status.
2. Municipality workers can inform the industries of the latest status.

</details>


## Choose municipality - UC-1.4
<details>
<b>ID: </b> UC-1.4<br>
<b>Priority: </b> High <br>
<b>Actors: </b>Municipality worker <br>
<b>Description: </b> The user must choose the municipality they’re working within, to get the relevant data when opening the program. <br>
<b>Trigger:  </b> Triggered when the program is opened and must be chosen before continuing.

---

<h4>Type: </h4>

-  [x] External 
-  [ ] Temporal 
---
<h4>Preconditions</h4>

1. The program opened successfully.

---
<h4>Normal Course</h4>

1. The user chooses municipality.
2. The program is set up for display of  relevant data.

---
<h4>Alternative Courses</h4>

1. The program did not open correctly.
2. Municipality could not be found.

---
<h4>Postconditions</h4>

1. All data is centered around the chosen municipality.
2. Municipality chosen

</details>

## Lockdown/restriction indicator - UC-1.5
<details>
<b>ID: </b> UC-1.4<br>
<b>Priority: </b> High <br>
<b>Actors: </b>Municipality worker <br>
<b>Description: </b> The program displays lockdown/restriction indicators based on incident numbers. <br>
<b>Trigger:  </b> Triggered when CSV data is uploaded.

---

<h4>Type: </h4>

-  [x] External 
-  [ ] Temporal 
---
<h4>Preconditions</h4>

1. CSV data is uploaded.
2. Municipality chosen.

---
<h4>Normal Course</h4>

1. Display current status lists of industries with dates of last changes.
2. The user can make decisions based on the indicators shown.

---
<h4>Alternative Courses</h4>

1. Error in database connection.
2. Error in CSV data.

---
<h4>Postconditions</h4>

1. The user can act on indicators on the given data.

</details>
