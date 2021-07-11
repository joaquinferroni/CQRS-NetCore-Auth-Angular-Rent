import { AfterViewInit, Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { Observable, Subscription } from 'rxjs';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit, AfterViewInit {
  @ViewChild('acc') panelsView!:any;
  @Output() onSetMarker = new EventEmitter<google.maps.LatLngLiteral>();
  @Input() viewMap:boolean=false;
  @Input() allowSetMarket:boolean=false;
  @Input() events!: Observable<any>;
  private addMarkerSubscription!: Subscription;
  center!: google.maps.LatLngLiteral;
  markers:any[]=[];
  zoom:number = 12;
  constructor() { 
  }
  ngAfterViewInit(): void {
    this.toggleMap();

  }

  ngOnInit(): void {
    if(this.events)
    this.addMarkerSubscription = this.events.subscribe(data => {
      if(!data.eventType || data.eventType ==='setMarker')
        this.onMarketAdd(data);
      else if(data.eventType ==='moveMarker'){
        this.setMarkerMove(true,data.lat,data.lng);
      }
      else if(data.eventType ==='stopMarker'){
        this.setMarkerMove(false,data.lat,data.lng);
      }
    });
    
    navigator.geolocation.getCurrentPosition((position) => {
      this.center = {
        lat: position.coords.latitude,
        lng: position.coords.longitude,
      }
    })
  }

  ngOnDestroy() {
    this.addMarkerSubscription.unsubscribe();
  }

 setMarkerClick(event: google.maps.MouseEvent){
  if(!this.allowSetMarket)return;
   this.markers = [];
    this.addMarker(event.latLng.lat(),event.latLng.lng(),'Current Market');
    this.onSetMarker.emit({
      lat: event.latLng.lat(),
      lng: event.latLng.lng(),
    });
  }

  addMarker(lat:number,long:number,title:string) {
    this.markers.push({
      position: {
        lat: lat ,
        lng: long ,
      },
      label: {
        color: 'white',
        text: title,
      },
      title: 'Marker title ' + (this.markers.length + 1),
      options: {  },//animation: google.maps.Animation.BOUNCE
    });
  }

  onMarketAdd(data:any){
    if(!data.lat) this.markers=[];
    else this.addMarker(data.lat,data.lng,data.title);
  }

  removeMarker(event:any){
    if(!this.allowSetMarket)return;
    this.markers = this.markers.filter(m => m.position.lat !== event.latLng.lat() && m.position.lng !== event.latLng.lng());
  }

  toggleMap(){
    if(!this.viewMap)return;
    this.panelsView.toggle('panel_map');
  }

  setMarkerMove(move:boolean,lat:number,lng:number){
    if(!move)return;
    this.zoom = 15;
    this.center = {
      lat: lat,
      lng: lng,
    }
  }
}
