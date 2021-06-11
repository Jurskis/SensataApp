import { Component } from '@angular/core';
import { AppService } from '../app.service'
import { VehicleInput } from '../vehicle-input';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-vehicle',
  templateUrl: './vehicle.component.html'
})
export class VehicleComponent {
  vehicleInputs: VehicleInput[];

  constructor(appService: AppService, route: ActivatedRoute) {
    appService.getVehicleInputs(route.snapshot.paramMap.get('vehicleId')).subscribe((result) => {
      this.vehicleInputs = result;
    });
  }
}
