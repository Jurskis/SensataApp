import { Component } from '@angular/core';
import { AppService } from '../app.service'
import { VehicleInput } from '../vehicle-input';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  latestVehicleInputs: VehicleInput[];

  constructor(appService: AppService) {
    appService.getLatestVehicleInputs().subscribe((result) => {
      this.latestVehicleInputs = result;
    });
  }

  addVehicle() {
    alert("add");
  }
}
