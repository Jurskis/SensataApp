import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  latestVehicleInputs: LatestVehicleInput[];
  names: string[];

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.http.get<LatestVehicleInput[]>(this.baseUrl + 'api/vehicles/latest').subscribe((result) => {
      this.latestVehicleInputs = result;
    });
  }

  addVehicle() {
    alert("add");
  }
}

interface LatestVehicleInput {
  vehicleId: string;
  vehicleName: string;
  latitude: string;
  longitute: string;
  speed: string;
}
