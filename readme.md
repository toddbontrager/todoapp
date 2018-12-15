If you are on the "authorization" branch, send a POST request to http://localhost:49965/api/token with a JSON object in the body with the following:

{"username": "todd", "password": "secret"}

The response will contain a token. Copy the value of this token and place in the header of the GET request: 

Key: Authorization
Value: Bearer [token]