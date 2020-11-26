import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { ErrorDetails, QueueTypeDto, QueueTypeModel, QueueTypesClient, SiteDto } from 'src/app/shared/clients';
import { SnackbarService } from 'src/app/shared/utilities/Snackbar.service';

@Component({
  selector: 'app-queue-types-page',
  templateUrl: './queue-types-page.component.html',
  styleUrls: ['./queue-types-page.component.scss']
})
export class QueueTypesPageComponent implements OnInit {
  dataSource: MatTableDataSource<QueueTypeDto>;
  columnsToDisplay = [ 'name', 'numberOfQueues' ];
  typeForm: FormGroup;

  types: QueueTypeDto[];
  selected: SiteDto;
  isNew = false;

  constructor(
    private typesClient: QueueTypesClient,
    private formBuilder: FormBuilder,
    private snackbar: SnackbarService
  ) {
    typesClient.getQueueTypes().subscribe(data => {
      this.types = data;
      this.dataSource = new MatTableDataSource<QueueTypeDto>(this.types);
      this.setFilter();
    },
    (error: ErrorDetails) => {
      this.snackbar.showSnackbar(error.message);
    });
  }

  ngOnInit(): void {
  }

  selectRow(row: SiteDto): void {
    this.selected = new SiteDto(row);
    this.isNew = false;

    this.typeForm = this.formBuilder.group({
      name: [this.selected.name, Validators.required]
    });
  }

  onSubmit(): void {
    if (!this.typeForm.valid) {
      return;
    }

    const model = new QueueTypeModel({name: this.typeForm.value.name});
    if (this.isNew){
      this.typesClient.createQueueType(model)
        .subscribe(created => {
          this.snackbar.showSnackbar('Queue type created');
          this.types.push(created);
          this.dataSource = new MatTableDataSource<SiteDto>(this.types);
          this.isNew = false;
          this.selected = null;
          this.setFilter();
        },
        (error: ErrorDetails) => {
          this.snackbar.showSnackbar(error.message);
        });
    } else {
      this.typesClient.updateQueueType(this.selected.id, model)
        .subscribe(() => {
          this.snackbar.showSnackbar('Queue type updated');
          const updated = this.types.find((item: SiteDto) => item.id === this.selected.id);
          updated.name = this.typeForm.value.name;
        },
        (error: ErrorDetails) => {
          this.snackbar.showSnackbar(error.message);
        });
    }
  }

  onNew(): void {
    this.isNew = true;
    this.selected = new SiteDto();
    this.typeForm = this.formBuilder.group({
      name: ['', Validators.required]
    });
  }

  onSearchChange(searchValue: string): void {
    console.log(searchValue);
    this.dataSource.filter = searchValue;
  }

  private setFilter(): void {
    this.dataSource.filterPredicate = (type: QueueTypeDto, filterText: string) => {
      return type.name.toLowerCase().indexOf(filterText.toLocaleLowerCase()) === 0;
    };
  }
}
