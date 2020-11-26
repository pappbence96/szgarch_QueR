import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { ApplicationUserDto, ErrorDetails, QueueDto, QueueModel, QueuesClient, QueueTypeDto, QueueTypesClient, SitesClient, UsersClient } from 'src/app/shared/clients';
import { SnackbarService } from 'src/app/shared/utilities/Snackbar.service';

@Component({
  selector: 'app-queues-page',
  templateUrl: './queues-page.component.html',
  styleUrls: ['./queues-page.component.scss']
})
export class QueuesPageComponent implements OnInit {
  dataSource: MatTableDataSource<QueueDto>;
  columnsToDisplay = [ 'type', 'number', 'step', 'employees', 'tickets' ];
  queueForm: FormGroup;

  employees: ApplicationUserDto[];
  types: QueueTypeDto[];
  selectedType: QueueTypeDto;

  queues: QueueDto[];
  selected: QueueDto;
  isNew = false;

  constructor(
    private queuesClient: QueuesClient,
    private sitesClient: SitesClient,
    queueTypeClient: QueueTypesClient,
    private formBuilder: FormBuilder,
    private dialog: MatDialog,
    private snackbar: SnackbarService
  ) {
    queueTypeClient.getQueueTypes().subscribe(data => {
      this.types = data;
      console.log(this.types);
    },
    (error: ErrorDetails) => {
      snackbar.showSnackbar(error.message);
    });
    sitesClient.getOwnEmployees().subscribe(data => {
      this.employees = data;
    },
    (error: ErrorDetails) => {
      snackbar.showSnackbar(error.message);
    });
    queuesClient.getQueues().subscribe(data => {
      this.queues = data;
      this.dataSource = new MatTableDataSource<QueueDto>(this.queues);
    },
    (error: ErrorDetails) => {
      this.snackbar.showSnackbar(error.message);
    });
  }

  ngOnInit(): void {
  }

  selectRow(row: QueueDto): void {
    this.selected = new QueueDto(row);
    this.isNew = false;

    var type = this.types.find(t => t.id === this.selected.queueTypeId);
    this.queueForm = this.formBuilder.group({
      type: [{ value: type, disabled: true}],
      start: [{ value: this.selected.nextNumber, disabled: true}],
      prefix: [this.selected.prefix],
      step: [this.selected.step, Validators.required]
    });
  }

  onSubmit(): void {
    if (!this.queueForm.valid) {
      return;
    }

    if (this.isNew){
      const model = new QueueModel({
        typeId: this.queueForm.value.type.id,
        nextNumber: this.queueForm.value.start,
        prefix: this.queueForm.value.prefix,
        step: this.queueForm.value.step});
      this.queuesClient.createQueue(model)
        .subscribe(created => {
          this.snackbar.showSnackbar('Queue created');
          this.queues.push(created);
          this.dataSource = new MatTableDataSource<QueueDto>(this.queues);
          this.isNew = false;
          this.selected = null;
        },
        (error: ErrorDetails) => {
          this.snackbar.showSnackbar(error.message);
        });
    } else {
      const model = new QueueModel({
        prefix: this.queueForm.value.prefix,
        step: this.queueForm.value.step});
      this.queuesClient.updateQueue(this.selected.id, model)
        .subscribe(() => {
          this.snackbar.showSnackbar('Queue updated');
          const updated = this.queues.find((item: QueueDto) => item.id === this.selected.id);
          updated.prefix = this.queueForm.value.prefix;
          updated.step = this.queueForm.value.step;
        },
        (error: ErrorDetails) => {
          this.snackbar.showSnackbar(error.message);
        });
    }
  }

  onNew(): void {
    this.isNew = true;
    this.selected = new QueueDto();
    this.queueForm = this.formBuilder.group({
      type: [this.selected.queueType, Validators.required],
      start: [this.selected.nextNumber, Validators.required],
      prefix: [this.selected.prefix],
      step: [this.selected.step, Validators.required]
    });
  }

  promoteManager(): void {/*
    const dialogRef = this.dialog.open(ConfirmDialogComponent);

    dialogRef.afterClosed().pipe(
      filter((result) => result),
      switchMap(() => this.queuesClient.assignManagerToSite(this.selected.id, this.selectedEmployee.id))
      ).subscribe(() => {
        this.snackbar.showSnackbar('Manager successfully promoted');
        const updated = this.queues.find((item: SiteDto) => item.id === this.selected.id);
        updated.managerName = this.selectedEmployee.userName;
        updated.managerId = this.selectedEmployee.id;
        this.selected = updated;
        this.selectedEmployee.worksiteId = this.selected.id;
      },
      (error: ErrorDetails) => {
        this.snackbar.showSnackbar(error.message);
      });*/
  }

  demoteManager(): void {/*
    const dialogRef = this.dialog.open(ConfirmDialogComponent);

    dialogRef.afterClosed().pipe(
      filter((result) => result),
      switchMap(() => this.queuesClient.removeManagerFromSite(this.selected.id))
      ).subscribe(() => {
        this.snackbar.showSnackbar('Manager successfully demoted');
        const updated = this.queues.find((item: SiteDto) => item.id === this.selected.id);
        updated.managerName = '-';
        updated.managerId = null;
        this.selected = updated;
        this.selectedEmployee = null;
      },
      (error: ErrorDetails) => {
        this.snackbar.showSnackbar(error.message);
      });*/
  }
}

