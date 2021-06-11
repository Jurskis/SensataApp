import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { VehicleInput } from './vehicle-input';

@Injectable({
  providedIn: 'root'
})
export class AppService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getLatestVehicleInputs() {
    return this.http.get<VehicleInput[]>(this.baseUrl + 'api/vehicles/latest');
  }

  getVehicleInputs(id: string) {
    return this.http.get<VehicleInput[]>(this.baseUrl + 'api/vehicles/' + id);
  }
}
