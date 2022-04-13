How long did you spend on the coding test? What would you add to your solution if you had more time? If you didn't spend much time on the coding test then use this as an opportunity to explain what you would add.
--> Web API - [I will add below functionality to the Web API]

---> Enable health check, this will verify the health of the application and can easily check the health of the dependent services. This will help to manage the availability of the API. Can easily manage in cluster/ App service

---> Handle Generic Exception handing. I will add exception handler middleware to handle the failed requests and format generic response and log all the detail information of the failure.

---> Add additional perf counter, this will help to monitor Web Api and its performance.

---> We can think of caching, at the moment we are not using any caching, so every request it will hit the just eat API, depending on the use case and the chances of updating the restaurant data, we can use caching, so the response from the API will be faster and we can meet the SLA of API response time.
     There are few ways we can easily warm up the cache and make sure that before application switch to live all required data is loaded into cache. for this we can use the .net core background process job.

---> I have added in memory functional tests. I will use this test suite at compile time and during the CI process. I have handled few use cases, But we can add some more test to meet all the business logic and test the response.

---> I will add few more test suits

-----> Acceptance Tests -> We can use this suite the run/execute tests post deployment on staging slots, here we can warm up all the instances of the application and execute test to make sure that post swap to production, API should work without failures.

-----> Unit Tests --> for Business critical cases, I will add few unit test to cover the use cases.

---------
React Web Application

this is very basic react app i used here to just display the result on the web, we can refactor components further to make it reusable components, uses of services instead of direct http request from components.

What was the most useful feature that was added to the latest version of your chosen language? Please include a snippet of code that shows how you've used it.
--> I used MS ASP.Net core web API this is the best framework for microservices, we can easily build, test and deploy with CICD.
--> We can customised the http request pipeline, Like middleware
--> Handling best way to use http client using dependency injection.
--> In built logging, and can be easily integrate with any log provider and easy to maintain. here I use ApplicationInsights, but we can use any log provider.
--> In Memory Intergation testing using WebApplicationFactory, No needs to deploy and run application on any web server. easy to test and substitute alternate for external dependency. Without external dependency like databases, API, we can test the full functionality of the application.
-->
How would you track down a performance issue in production? Have you ever had to do this?
---> I have already mentiond in the TODO list, we can add additional perf counters and metrics to application insights so that we can monitor the live performance of the code logic and external dependencies. this will help to tract the each process and its performance time, we can easily build the dashboard using log analytics, and monitor the production performance.

I did many times, actually when I develop any microservice, it has to go through the full perf testing/ load testing to identity the performance, memory leacks in deployed envts.
I investgated memory leack problem in the developed SDK for Azure Service BUS, And due to this the deployed application instance consumes more memory.

How would you improve the Just Eat APIs that you just used?

--> The current API has less filters to query and get the data, As per the web application need, I will add few more filter parameters so that the data across the network is less and it improves the performance of the API as will as the web application.