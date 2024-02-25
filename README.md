# SV-Coding-Case
## Overview
This project provides a dynamic search functionality for a web application, leveraging a RESTful API to fetch and display search results based on user input. It involves a frontend interface that interacts with a backend service to query and retrieve relevant data.

## Features
- Dynamic Search: Real-time search results as the user types.
- Result Sorting: Orders items based on their relevance and weight.
- Icon Display: Each item type (Lock, Building, Group, Medium) is represented with a unique icon for easy identification.
- Styling: Enhanced user interface with CSS for readability and user experience.

## Technologies Used
- HTML & CSS for the frontend design.
- Vanilla JavaScript for client-side scripting.
- ASP.Net RESTful API for backend interaction.

## Setup
- Clone the repository.
- Open terminal
- Navigate to SV-CodingCase folder 
`cd SV-CodingCase`
- Build docker image 
`docker build . -t sv-codingcase`
- Run docker image
`docker run -p 8080:8080 sv-codingcase`
- Open your browser and navigate to 
`http://localhost:8080/`

For detailed instructions and more information, refer to the individual files and code comments within the project.
