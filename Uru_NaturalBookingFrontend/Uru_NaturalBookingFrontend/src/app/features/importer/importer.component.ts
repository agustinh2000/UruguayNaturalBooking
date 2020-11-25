import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Parameter } from 'src/app/models/Parameter';
import { ImporterService } from '../services/importer.service';

@Component({
  selector: 'app-importer',
  templateUrl: './importer.component.html',
  styleUrls: ['./importer.component.css'],
})
export class ImporterComponent implements OnInit {
  public importersAvailablesName;

  public formGroup: FormGroup;

  constructor(
    private importerService: ImporterService,
    private formBuilder: FormBuilder,
    private router: Router
  ) {
    this.importersAvailablesName = new Array();
    this.formGroup = this.formBuilder.group({
      pathForImport: ['', [Validators.required]],
      selectedImporter: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
    this.getTheNamesOfImportersAvailables();
  }

  private getTheNamesOfImportersAvailables(): void {
    this.importerService.getImportersName().subscribe(
      (res) => {
        this.importersAvailablesName = res;
      },
      (err) => {
        alert(err);
      }
    );
  }

  public import(): void {
    let nameOfFileImporter: string;
    const importerNameSelected = this.formGroup.controls.selectedImporter.value;
    if (importerNameSelected === 'Importador JSON') {
      nameOfFileImporter = 'Importers\\ImporterJson.dll';
    } else {
      nameOfFileImporter = 'Importers\\ImporterXml.dll';
    }
    const parameters: Parameter[] = [
      {
        name: 'path',
        type: 'file',
        value: this.formGroup.controls.pathForImport.value,
      },
    ];
    this.importerService
      .importLodgings(parameters, nameOfFileImporter)
      .subscribe(
        (res: string) => {
          alert(res);
          this.router.navigate(['/regions']);
        },
        (err) => {
          alert(err.error);
        }
      );
  }
}
