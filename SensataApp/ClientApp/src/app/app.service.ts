import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { VehicleInput } from './vehicle-input';
import { Vehicle } from './vehicle';
import { Observable } from 'rxjs';
import { catchError, take } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AppService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getLatestVehicleInputs(): Observable<VehicleInput[]> {
    return this.http.get<VehicleInput[]>(this.baseUrl + 'api/vehicleinputs/latest')
      .pipe(
        take(1),
        catchError((err) => { throw (err) })
      );
  }

  getVehicleInputs(id: string): Observable<VehicleInput[]> {
    return this.http.get<VehicleInput[]>(this.baseUrl + 'api/vehicleinputs/' + id)
      .pipe(
        take(1),
        catchError((err) => { throw (err) })
      );
  }

  addVehicle(vehicle: Vehicle): Observable<Object> {
    return this.http.post(this.baseUrl + 'api/vehicles', vehicle)
      .pipe(
        take(1),
        catchError((err) => { throw (err) })
      );
  }
}
