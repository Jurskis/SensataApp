import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public latestVehicleInputs: LatestVehicleInput[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<LatestVehicleInput[]>(baseUrl + 'api/vehicles/latest').subscribe(result => {
      this.latestVehicleInputs = result;
    }, error => console.error(error));
  }

  AddVehicle() {
    alert("add");
  }
}

interface LatestVehicleInput {
  VehicleId: string;
  VehicleName: string;
  Latitude: string;
  Longitute: string;
  Speed: string;
}
