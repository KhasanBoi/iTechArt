import { Component, Input } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

import { UnitsEnum } from '../../../../shared/enums/units.enum';
import { ApiService } from '../../../../shared/services/api.service';
import { ContentTypeService } from '../../../../shared/services/content-type.service';
import { ExtensionsEnum } from '../../../../shared/enums/extensions.enum';

@Component({
  selector: 'app-excel-export',
  templateUrl: './excel-export.component.html',
  styleUrls: ['./excel-export.component.scss'],
})
export class ExcelExportComponent {
  @Input() public unit: UnitsEnum | undefined;

  public api: string | undefined;
  public fileName: string | undefined;
  public isFormValid = false;
  public isFormButtonDisabled = false;
  public fileExtension: string | undefined;

  public exportForm = new FormGroup({
    file: new FormControl('', [Validators.required]),
    fileSource: new FormControl('', [Validators.required]),
  });

  public constructor(
    private http: HttpClient,
    private apiService: ApiService,
    private contentTypeService: ContentTypeService
  ) {}

  public onFileChange(event: any): void {
    this.isFormButtonDisabled = false;

    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      this.fileName = file.name;
      const fileExtension = this.contentTypeService.getFileExtension(
        this.fileName
      ); //refactor

      this.fileExtension = fileExtension;

      this.api = this.apiService.defineImportApiForCurrentUnit(
        this.unit,
        this.fileExtension
      );

      if (
        fileExtension === ExtensionsEnum.csv ||
        fileExtension === ExtensionsEnum.xls ||
        fileExtension === ExtensionsEnum.xml ||
        fileExtension === ExtensionsEnum.xlsx
      ) {
        this.exportForm.patchValue({
          fileSource: file,
        });
        this.isFormValid = true;
      } else {
        this.isFormValid = false;
      }
    }
  }

  public submit(): void {
    this.isFormButtonDisabled = true;
    const file = this.exportForm.get('fileSource')!.value;
    const formData = new FormData();
    formData.append('file', file!);

    console.log(this.api);

    this.http.post<any>(`${this.api}`, formData).subscribe({
      next: (data: string) => {
        alert('Uploaded Successfully.');
        console.log(data);
        this.isFormButtonDisabled = false;
        location.reload();
      },
      error: (error: string) => {
        alert(`There was an error! ${error}`);
        console.log(error);
        this.isFormButtonDisabled = false;
      },
    });
  }
}
