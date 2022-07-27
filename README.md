# SwiftRehabAppBackEnd

ASP.NET core web API made to work with the final year dissertation project Swift Rehab Application which was written in React Native.

Instructions for running the back end API

1. Install VS 2022 with .net 6
2. Install docker desktop
3. Build the C# code
4. Open the docker-compose project in powershell
5. Run the command "docker-compose -f docker-compose.yml -f docker-compose.override.yml up --build"


For the python set up (Running the python scripts outside of docker containers)
1. Install python 3.9
2. Install pip
3. cd into the requirements folder
4. Run command pip install -r requirements.txt (This will install all required python packages)
5. Run the python scripts by cd'ing into the folder and doing 'python SCRIPT_NAME.py' (Make sure the rabbitmq container is running first)