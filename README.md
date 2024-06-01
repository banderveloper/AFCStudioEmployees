# AFCStudioEmployees

This repository contains a simple fullstack web-application.
Project built on [this specification](https://gist.github.com/paraekklisiarh/0621204ce249e9faf1aaa1e1b7d3f7ef)

## Description

This project is a full-stack web application built using:

- **Backend:** ASP.NET Core
- **Frontend:** React + Typescript

The backend provides a set of RESTful API endpoints, while the frontend consumes these APIs.

## Features

- CRUD operations for managing data
- Pagination
- Docker-compose orchestration
- Automated docker containers build using Github CI-CD
- Client-side routing using React Router
- State management using Zustand

## Prerequisites

To run this project locally, you need to have the following installed:

- [Docker](https://www.docker.com/products/docker-desktop/)
- [Node.js](https://nodejs.org/en/download/)
- [npm](https://www.npmjs.com/get-npm)

## Getting Started

Follow these instructions to get the project up and running on your local machine.

### Clone the Repository

```bash
git clone https://github.com/banderveloper/AFCStudioEmployees
cd AFCStudioEmployees
```

### Run docker-stored backend
```bash
cd src/AFCStudioEmployees.Backend
docker-compose up
```

### Run frontend on port 3000
```bash
cd ../AFCStudioEmployees.Frontend
npm run dev
```

### Go to [local web-page](http://localhost:3000/employees) and test

## Contact
If you have any questions, create issue, or contact me using [Instagram](https://www.instagram.com/banderveloper) or Telegram (@bandernik)
