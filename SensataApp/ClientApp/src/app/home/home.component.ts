import { Component, OnDestroy, OnInit, TemplateRef } from '@angular/core';
import { AppService } from '../app.service'
import { VehicleInput } from '../vehicle-input';
import { Vehicle } from '../vehicle';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Subscription, Observable } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit, OnDestroy {
  latestVehicleInputs: VehicleInput[];
  newVehicle: Vehicle;
  inputs$: Observable<VehicleInput[]>;
  vehicleAddSub: Subscription;

  page: number = 1;

  constructor(private appService: AppService, private modalService: NgbModal) { }

  ngOnInit(): void {
    this.inputs$ = this.appService.getLatestVehicleInputs();
  }

  ngOnDestroy(): void {
    if (this.vehicleAddSub)
      this.vehicleAddSub.unsubscribe();
  }

  open(content: TemplateRef<any>): void {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });
  }

  addVehicle(vehicle: Vehicle): void {
    // If there is a previuos subscription, unsubscribe from it.
    if (this.vehicleAddSub)
      this.vehicleAddSub.unsubscribe();

    this.vehicleAddSub = this.appService.addVehicle(vehicle).subscribe((response) => {
      this.inputs$ = this.appService.getLatestVehicleInputs(); // Refresh data.
    });
    
    this.modalService.dismissAll(); // Dismiss all modals
  }
}
