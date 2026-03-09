# CUBE Recreation Guidelines 

I put this document together to outline everything we need to know to get started. The goal here is to keep our codebase clean, avoid those massive merge conflicts, and make sure we can actually work on this project together efficiently.

## Table of Contents
1. [Getting Started](#getting-started)
2. [Git and GitHub Workflow](#git-and-github-workflow)
3. [Project Structure and Architecture](#project-structure-and-architecture)
4. [Coding Standards and Syntax](#coding-standards-and-syntax)
5. [Keeping the Code Updated](#keeping-the-code-updated)
6. [Branch and Commit Conventions](#branch-and-commit-conventions)

## Getting Started

You should only have to go through this setup once. 

If you are on Windows, you can install the required tools by opening PowerShell and running: <br>
`winget install Git.Git Microsoft.DotNet.SDK.8 Node.js`

After this, you can clone the repository to your local machine:
`git clone <link_to_repo>`

**Setting up the Frontend (Vue.js):**
1. Navigate to the frontend folder: `cd Frontend`
2. Install the required Node packages: `npm install`
3. Start the local development server: `npm run dev`

**Setting up the Backend (.NET API):**
1. Open a new terminal and navigate to the backend folder: `cd Backend`
2. Start the local API server: `dotnet run`

## Git and GitHub Workflow

> [!CAUTION]
> Please do not push code directly to the `main` branch. Always create a new branch for your specific feature so we can review code safely before it goes live.

For each feature you want to add (listed on our Trello board), please follow these steps:

### 1) Create a new branch
Create and switch to a new branch for your task. For example:<br>
`git checkout -b feature/navbar-implementation`

### 2) Make your changes
Open the project in your code editor and build out your feature.

### 3) Save your changes to Git and GitHub
Stage your files and commit them with a clear message: <br>
`git add .` <br>
`git commit -m "Implement a navbar throughout the whole website"` <br>

Then push your specific branch to the remote repository: <br>
`git push origin feature/navbar-implementation`

### 4) Create a Pull Request (PR)
Once your branch is finalized and working locally, go to the repository page on GitHub.
1. You will see a prominent green button that says "Compare & pull request". Click it.
2. Add a brief description of what you changed.
3. Click "Create pull request".

## Project Structure and Architecture

The project is split up into two distinct parts: the frontend and the backend. Each of those is contained in its own folder.
* `Frontend/` contains the Vue.js code.
* `Backend/` contains the .NET C# API.

The backend will automatically deploy to the remote server whenever a new commit or merge happens on the main branch.

> [!IMPORTANT]
> **Database Rule:** We are making a code-first API. If you update the C# model, the database also needs to migrate. Please make sure to message everyone on Discord before running `dotnet ef migrations add`. If multiple people are working on migrations at the same time, the database will break. To prevent this, we will coordinate and do one migration at a time, purely from the MAIN BRANCH.

### About the Frontend
Every time you pull new code, you might need to install updated project dependencies. Just to be safe, it helps to run:
`npm install`

## Coding Standards and Syntax

> [!IMPORTANT]
> We need to follow this syntax so our codebase stays clean and readable for everyone. PRs that don't match this formatting will need to be revised before merging, and I really don't want anyone to have to redo their hard work!

### Spacing and Indentation
* Please make sure your code is properly indented.
* Indentation must always be 2 spaces. No tabs.
* Try to avoid adding more than one empty line. Let's keep the code compact but readable.

### Naming Conventions and Braces
We are adopting these principles to keep the codebase entirely uniform and predictable for the whole team. 

* **Allman Style Braces:** Every opening brace `{` and closing brace `}` must sit on its own dedicated line. This creates a visually distinct block of code and aligns the braces vertically. 
* **camelCase:** The first letter of the first word is lowercase, and the first letter of every subsequent word is capitalized (e.g., `myLocalVariable`). We use this for local variables, function parameters, and private fields.
* **PascalCase:** The first letter of every word is capitalized, including the very first word (e.g., `MyPublicClass`). We use this for classes, methods, and public properties.

### Examples
Here is a complete C# example:

```c#
using System;

namespace CodingStandards
{
  public class EmployeeRecord
  {
    private string _employeeName;  // private variables start with an underscore and use camelCase
    public int EmployeeId { get; set; } // public variables use PascalCase
    
    public EmployeeRecord(string name, int id)
    {
        _employeeName = name;
        EmployeeId = id;
    }
    
    public void PrintDetailedReport(string reportTitle, string departmentName, bool includeInactive)
    { // Allman Style Braces EVERYWHERE.
      bool isActive = true;  // local variables also use camelCase
      
      if (isActive)
      {
        // Very long expressions or method calls also wrap to the next line
        string output = string.Format(
          "Name: {0}, ID: {1}, Dept: {2}, Title: {3}",
          _employeeName,
          EmployeeId,
          departmentName,
          reportTitle);
          
        Console.WriteLine(output);
      }
    }
  }
}

```

And a JS example:

```html
<script setup>
import { ref } from 'vue';

// local variables use camelCase
const employeeName = ref('Jane Doe');
const employeeId = ref(101);

// Allman Style Braces EVERYWHERE
const printDetailedReport = (reportTitle, departmentName, includeInactive) =>
{
  if (employeeId.value > 0)
  {
    // Very long expressions or method calls also wrap to the next line
    console.log(
      "Name: " + employeeName.value,
      "ID: " + employeeId.value,
      "Dept: " + departmentName,
      "Title: " + reportTitle
    );
  }
};

const getEmployeeData = () =>
{
  // A quick JavaScript limitation to note:
  // You cannot use an Allman brace directly after a 'return' statement.
  // JavaScript will automatically insert a semicolon after 'return', breaking the code.
  // The opening brace for a returned object must stay on the same line.
  return {
    fullName: employeeName.value,
    idNumber: employeeId.value,
    status: 'Active'
  };
};
</script>

<template>
  <div class="employee-card">
    <h2>{{ employeeName }}</h2>
    
    <button
      class="btn-primary"
      type="button"
      @click="printDetailedReport('Q3 Report', 'Engineering', false)"
    >
      Print Data
    </button>
  </div>
</template>

```

## Keeping the Code Updated

As you work, other teammates will get their PRs merged into main. You need to regularly bring those updates into your own branch so you do not fall behind.
This step is super important. Every time you start coding, and every time you finish implementing a feature, make sure to do this:

1. Switch back to the main branch: `git checkout main`
2. Download the latest updates: `git pull origin main`
3. Switch back to your working branch: `git checkout feature/name-of-your-task`
4. Merge the new main updates into your branch: `git merge main`

## Branch and Commit Conventions

To keep our repository organized, let's stick to these naming conventions.

### Branch Names

Please prefix your branch name with the type of work you are doing, followed by a brief description separated by hyphens.

* `feature/` for new additions (`feature/login-system`)
* `bugfix/` for fixing issues (`bugfix/navbar-alignment`)
* `tests/` for writing unit or moq tests (`tests/login-service`)
* `docs/` for writing or updating documentation (`docs/update-readme`)

### Commit Messages

Write your commit messages in the imperative mood. Think of it as giving a command to the codebase.

* **Good:** `Add user authentication backend logic`
* **Good:** `Fix broken image link on homepage`
* **Bad:** `Added user authentication`
* **Bad:** `Fixing the images`
