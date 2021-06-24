import { Injectable, Inject } from '@angular/core';
import * as signalR from '@microsoft/signalr'

@Injectable({
  providedIn: 'root'
})
export class SignalRService {

  public hubConn: signalR.HubConnection;

  constructor(@Inject('BASE_URL') private baseUrl: string) { }

  startConnection(): void {
    this.hubConn = new signalR.HubConnectionBuilder()
      .withUrl(this.baseUrl + 'vehicleinputs')
      .build();

    this.hubConn
      .start()
      .catch(err => console.error('Error occured while starting connection to Vehicle Input hub. ' + err));
  }

  closeConnection(): void {
    this.hubConn.stop();
  }
}
