

export class ApartmentModel {
    id: number = 0;
    name:string = '';
    description: string = '';
    floor: number = 0;
    size:number = 0;
    price:number= 0 ;
    rooms: number= 0;
    latitude: number= 0;
    longitude:number= 0;
    created_At!: Date ;
    status:string = 'AVAILABLE';
    userName:string='';
    constructor() {
        
    }
}



export class ApartmentFilter {
    sizeFrom!:number;
    sizeTo!:number;
    priceFrom!:number;
    priceTo!:number;
    roomsFrom!:number;
    roomsTo!:number;
    status:string='';
    constructor() {
        
    }
}
