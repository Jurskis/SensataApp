import { Component, OnDestroy, OnInit } from '@angular/core';
import { AppService } from '../app.service'
import { SignalRService } from '../signalr.service';
import { VehicleInput } from '../vehicle-input';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-vehicle',
  templateUrl: './vehicle.component.html'
})
export class VehicleComponent implements OnInit, OnDestroy {
  success: boolean = true; // Successfully got data.
  vehicleInputs: VehicleInput[];
  vehicleName: string;
  page: number = 1;

  constructor(private appService: AppService, private signalRService: SignalRService, private route: ActivatedRoute) {}

  getVehicleInputs(): void {
    this.appService.getVehicleInputs(this.route.snapshot.paramMap.get('vehicleId'))
      .subscribe(
        data => { this.vehicleInputs = data },
        error => { this.success = false; }
      );
  }

  ngOnInit(): void {
    this.vehicleName = this.route.snapshot.paramMap.get('vehicleName');

    this.getVehicleInputs();

    this.signalRService.startConnection();
    this.signalRService.hubConn.on('vehicleinputadded', () => {
      this.getVehicleInputs();
    })
  }

  ngOnDestroy(): void {
    this.signalRService.closeConnection();
  }
}
