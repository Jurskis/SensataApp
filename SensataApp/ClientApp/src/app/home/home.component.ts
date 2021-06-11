import { Component } from '@angular/core';
import { AppService } from '../app.service'
import { VehicleInput } from '../vehicle-input';
import { Vehicle } from '../vehicle';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  latestVehicleInputs: VehicleInput[];
  newVehicle: Vehicle;

  constructor(private appService: AppService, private modalService: NgbModal) {}

  open(content) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });
  }

  addVehicle(data) {
    console.log(data);
    this.newVehicle = { name: data.vehicleName };
    //this.newVehicle.name = data.vehicleName;
    this.appService.addVehicle(this.newVehicle).subscribe((result) => {
      this.ngOnInit(); // Reload
    });
    this.modalService.dismissAll(); // Dismiss all modals
  }

  ngOnInit() {
    this.appService.getLatestVehicleInputs().subscribe((result) => {
      this.latestVehicleInputs = result;
    });
  }
}
