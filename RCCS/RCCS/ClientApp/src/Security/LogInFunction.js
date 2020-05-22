let user = {
    personaleId: "",
    password: ""

};

export async function LogInFunction(context) {
    let userStringified = JSON.stringify(user);
    const that = context;
    fetch('https://localhost:44356/api/User/login', {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: userStringified
    })
        .then(function (response) {
            if (response.ok) {
                response.json().then(function (data) {
                    console.log("json: " + data);
                    console.log("jwt: " + data["jwt"]);
                    localStorage.setItem("jwt", data["jwt"]);
                    //end of test field
                    console.log('Logged in');
                    that.setState({
                        UserLoggedIn: true
                    });
                });
            } else {
                alert("HTTP-Error: " + response.status);
                console.log('Error - not logged in');
                that.setState({
                    Error: true
                });
            }
        }).catch(error => {
        console.error('Caught error:', error);
        that.setState({
            Error: true
        });
    });
    return user;
}
