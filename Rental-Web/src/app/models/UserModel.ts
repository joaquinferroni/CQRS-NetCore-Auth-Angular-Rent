

export class UserModel {
    id: number;
    userName: string;
    role: string;
    name!: string;
    password!: string;
    /**
     *
     */
    constructor(_id: number,_userName: string, _role: string) {
        this.userName = _userName;
        this.role = _role;
        this.id =_id;
    }
}
