import { Component } from '@angular/core';
import { AppService } from '../app.service'
import { VehicleInput } from '../vehicle-input';
import { ActivatedRoute } from '@angular/router';
import { error } from 'console';

@Component({
  selector: 'app-vehicle',
  templateUrl: './vehicle.component.html'
})
export class VehicleComponent {
  success: boolean = true; // Successfully got data. 
  vehicleInputs: VehicleInput[];
  vehicleName: string;

  constructor(appService: AppService, route: ActivatedRoute) {
    appService.getVehicleInputs(route.snapshot.paramMap.get('vehicleId')).subscribe(
      result => {
        this.vehicleInputs = result;
        this.vehicleName = route.snapshot.paramMap.get('vehicleName');
      },
      error => {
        this.success = false;
      }
    );
  }
}
