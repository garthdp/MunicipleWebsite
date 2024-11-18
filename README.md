# PROG7312 Part 1

A municiple system which allows users to create and view reports.

## Getting Started

### Installation

* On github click the <> Code button then click download zip. Once zip is downloaded unzip it.

### Executing program

* Once the project is unzipped run it using visual studio.
* To access features please sign in. The user accounts can be found in the homecontroller class or you can sign in with the username: Garth and password: password.

#### Read Below to learn more about how the app works and the functionality of each page.

## About the app

### Sign in screen
![image](https://github.com/user-attachments/assets/2e1bae8e-9668-4592-89ad-c3ea568ca936)
* Screen users must input there information to access features of the app. Other features can only be access once they have signed in.

### Home Screen
![image](https://github.com/user-attachments/assets/40d5e2b3-e0dd-45bd-8aed-90e83fe6f601)
* This is the homescreen where users will be able to access the different features. Only the Report Issues button works for part 1.

### Report Issues screen
![image](https://github.com/user-attachments/assets/ea619dcc-c8bf-4db2-b381-3dfebbca0345)
* Here users will be able to report issues. All fields must be filled in for the report to be made, this includes the location, the category, a discription and a file which could add extra context.
* The uploaded files are saved to the file "uploads" inside the project, in the wwwroot folder.

![image](https://github.com/user-attachments/assets/67f3b283-b676-4ed1-9979-bdb685cb65fb)
* This page also has a progress bar which fills up as the user inputs data.

### Reports list
![image](https://github.com/user-attachments/assets/c6298ef8-e099-4794-a612-7900705275dd)
* Here users can view reports made.
* It also has a button which allows the user to navigate to the create reports page.

### Leaderboards Page
![image](https://github.com/user-attachments/assets/1f64c521-04ac-4e1a-a4a5-6017a56aba87)
* This page shows the users with the highest levels.
* Users can gain experience to level up by completing tasks.

### Profile Page
![image](https://github.com/user-attachments/assets/520f4c96-56ac-48f5-9a27-9ed6d664cf00)
* Here users can view their level, experience are able to sign out.

### Error page
![image](https://github.com/user-attachments/assets/cae9d668-e37c-4b1d-8676-7793c8d71417)
* This page shows an error when trying to access features which have not been made yet such as the other two buttons on home page.

### Other Error Handeling
![image](https://github.com/user-attachments/assets/e8cfe19d-0ed1-4244-8fe2-264965d833a0)
![image](https://github.com/user-attachments/assets/8dbb6f3c-70b5-4df3-a7d4-038f9f5fee46)
* Error messages when the user doesn't input information into input boxes.

### Events Page
![image](https://github.com/user-attachments/assets/1679182d-555a-4eda-8df8-06fe0b508f35)
* This page will allow the user to search for events and then eventually start recommending events based on user searchs.

### Service Requests
![image](https://github.com/user-attachments/assets/2340ed7a-b3ee-4ab3-823d-6aff27baac3c)
* This page shows all service requests which are stored in a tree.
* This page also has a dependancy button which when pressed will show which other service requests the request is dependant on for it to be completed.

### Dependancy Page
![image](https://github.com/user-attachments/assets/2b05f39d-966e-478f-9dfb-1f034252abd8)
* This page shows which services requests the selected service request is dependant on.
* It also shows how far from completion the request is.

