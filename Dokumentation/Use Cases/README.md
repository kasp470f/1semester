# Use Case Overview

| Done? | UC ID  | Use Case                                           |
|:-----:|--------|----------------------------------------------------|
| [x]   | [UC-1.1](#display-of-relevant-data----uc-11) | Display of relevant data                            |
| [x]   | [UC-1.2](#upload-of-csv-files---uc-12) | Upload of CSV files                                 |
| [x]   | [UC-1.3](#addremove-industries-to-lockdownrestrictions-list---uc-13) | Add/remove industries to lockdown/restrictions list |
| [x]   | [UC-1.4](#choose-municipality---uc-14) | Choose municipality                                 |
| [x]   | [UC-1.5](#lockdownrestriction-indicator---uc-15) | Lockdown/restriction indicator                      |
| [x]   | [UC-1.6](#display-general-restrictions---uc-16) | Display general restrictions                        |

<br><br>

# Use Cases

## Display of relevant data  - UC-1.1
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

---

## Upload of CSV files - UC-1.2
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
3. User clicks the CSV upload button.
4. Confirmation message that file is uploaded successfully.


---
<h4>Alternative Courses</h4>

1. User has not chosen CSVs.
2. Failed to load data/locate the files.
3. CSV upload is unresponsive.

---
<h4>Postconditions</h4>

1. Files are successfully uploaded and the data can be used.

---

## Add/Remove industries to lockdown/restrictions list - UC-1.3
<b>ID: </b> UC-1.3<br>
<b>Priority: </b> High <br>
<b>Actors: </b>Municipality worker <br>
<b>Description: </b> Ability to add/remove industries to lockdown/restrictions list. <br>
<b>Trigger:  </b> The app has given a suggestion to add/remove industries from lockdown/restrictions list based on the latest corona data.

---

<h4>Type: </h4>

-  [x] External 
-  [ ] Temporal 
---
<h4>Preconditions</h4>

1. The user has picked their municipality.
2. The municipality worker has concluded, based on the data for the municipality (trends in positive %, amount sick, people in hospital/intensive care, dead) for 7 days, 14 days, month, compares to country wide statistics and suggestions given by the program, that an industry can either be locked down or restricted or be opened again.
3. CSV data is up to date.



---
<h4>Normal Course</h4>

1. The user can view the lockdown/restrictions list and add/remove to it.
2. User adds/removes an industry code to/from the lockdown/restrictions list from the list of all industries.
3. Confirmation box displays success message.
4. List refreshes and the user can now see the added restrictions/lockdowns.



---
<h4>Alternative Courses</h4>

1. CSV data is outdated
    1. Warns about CSV’s latest date vs. current date and suggests to double check CSV file choice
2. Municipality is not chosen
    1. Municipality choice is displayed until chosen
3. Error in database connection on insert/change/delete
    1. Displays error and box pops up asking user to retry
    2. Returns to municipality view



---
<h4>Postconditions</h4>

1. The different industries have relevant lockdown/restriction status.
2. Municipality workers can inform the industries of the latest status.

---


## Choose municipality - UC-1.4
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

---

## Lockdown/restriction indicator - UC-1.5
<b>ID: </b> UC-1.5<br>
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

1. Display current indicator based on the CSV data.
2. The user can make decisions based on the indicators shown.


---
<h4>Alternative Courses</h4>

1. Error in database connection.
2. Error in CSV data.

---
<h4>Postconditions</h4>

1. The user can act on indicators on the given data.

---

## Display general restrictions - UC-1.6
<b>ID: </b> UC-1.6<br>
<b>Priority: </b> High <br>
<b>Actors: </b>Municipality worker <br>
<b>Description: </b> Shows general restrictions that can be put in use. <br>
<b>Trigger:  </b> Show if there are negative tendencies in COVID-19 cases and restrictions are indicated.

---

<h4>Type: </h4>

-  [x] External 
-  [ ] Temporal 
---
<h4>Preconditions</h4>

1. CSV is uploaded and up to date.
2. There are negative tendencies and indicators based on the uploaded data.

---
<h4>Normal Course</h4>

1. The user gets a list of general restrictions to use.

---
<h4>Alternative Courses</h4>

1. User has not chosen CSVs.
2. Failed to load data.


---
<h4>Postconditions</h4>

1. The user can choose any of the recommended restrictions and act as they see fit  based on the data.
