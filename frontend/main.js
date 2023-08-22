window.addEventListener("DOMContentLoaded", (event) => {
  getVisitCount();
});

const functionApiUrl =
  "https://getresumecounter-aaj.azurewebsites.net/api/getresumecounter?code=PyHbUoIqGVgFJq71cLyMpoeYkeRLUOQ9iODPu56yJH6dAzFuvZqf8g%3D%3D";
const localFunctionApi = "http://localhost:7071/api/GetResumeCounter";

const getVisitCount = () => {
  let count = -1;
  fetch(functionApiUrl)
    .then((response) => {
      return response.json();
    })
    .then((response) => {
      console.log("Website called function API.");
      count = response.count;
      document.getElementById("counter").innerText = count;
    })
    .catch(function (error) {
      console.log(error);
    });
  return count;
};
