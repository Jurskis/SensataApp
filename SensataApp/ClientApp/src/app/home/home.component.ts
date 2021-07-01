import { Component, OnDestroy, OnInit, TemplateRef } from '@angular/core';
import { AppService } from '../app.service'
import { VehicleInput } from '../vehicle-input';
import { Vehicle } from '../vehicle';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { SignalRService } from '../signalr.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, OnDestroy {
  latestVehicleInputs: VehicleInput[];
  startingLat: number = 24.0;
  startingLng: number = 9.0;

  constructor(private appService: AppService, private signalRService: SignalRService, private modalService: NgbModal) { }

  private getLatestVehicleInputs(): void {
    this.appService.getLatestVehicleInputs()
      .subscribe(
        data => { this.latestVehicleInputs = data },
        error => { console.error('Error occured while getting latest vehicle inputs. ' + error) }
      );
  }

  ngOnInit(): void {
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
