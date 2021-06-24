import { Component, OnDestroy, OnInit, TemplateRef } from '@angular/core';
import { AppService } from '../app.service'
import { VehicleInput } from '../vehicle-input';
import { Vehicle } from '../vehicle';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { SignalRService } from '../signalr.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit, OnDestroy {
  //inputs$: Observable<VehicleInput[]>;
  latestVehicleInputs: VehicleInput[];
  page: number = 1;

  constructor(private appService: AppService, private signalRService: SignalRService, private modalService: NgbModal) { }

  getLatestVehicleInputs(): void {
    this.appService.getLatestVehicleInputs()
      .subscribe(
        data => { this.latestVehicleInputs = data },
        error => { console.error('Error occured while getting latest vehicle inputs. ' + error) }
      );
  }

  ngOnInit(): void {
    //this.inputs$ = this.appService.getLatestVehicleInputs();

    this.getLatestVehicleInputs();

    this.signalRService.startConnection();
    this.signalRService.hubConn.on('vehicleinputadded', () => {
      this.getLatestVehicleInputs();
    })
  }

  ngOnDestroy(): void {
    this.signalRService.closeConnection();
  }

  // Opens "Vehicle Add" modal.
  open(content: TemplateRef<any>): void {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });
  }

  addVehicle(vehicle: Vehicle): void {
    this.appService.addVehicle(vehicle).subscribe(() => {
      this.getLatestVehicleInputs(); // Refresh data.
    });
    
    this.modalService.dismissAll(); // Dismiss all modals
  }
}
