export class Auth{
    userName: string;
    password: string;
    /**
     *
     */
    constructor(_userName:string,_password:string) {
        this.userName = _userName;
        this.password = _password;
    }
}

export class AuthResponse{
    token: string;
    /**
     *
     */
    constructor(token:string) {
        this.token = token;
    }
}


