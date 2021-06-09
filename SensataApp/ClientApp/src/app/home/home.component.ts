import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public latestVehicleData: LatestVehicleData[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<LatestVehicleData[]>(baseUrl + 'api/vehicles/latest').subscribe(result => {
      this.latestVehicleData = result;
    }, error => console.error(error));
  }

  AddVehicle() {
    alert("add");
  }
}

interface LatestVehicleData {
  VehicleName: string;
  Latitude: string;
  Longitute: string;
  Speed: string;
}
