import {Component, OnInit} from '@angular/core';
import {MatDialogRef} from "@angular/material/dialog";
import {CreateTestRequest} from "../../../../../api/models/create-test-request";
import {FormArray, FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {MatIconRegistry} from "@angular/material/icon";
import {DomSanitizer} from "@angular/platform-browser";
import {DictionaryService} from "../../../../../api/services/dictionary.service";
import {DictionaryItemDto} from "../../../../../api/models/dictionary-item-dto";
import {TestService} from "../../../../../api/services/test.service";

@Component({
  selector: 'app-add-test-dialog',
  templateUrl: './add-test-dialog.component.html',
  styleUrls: ['./add-test-dialog.component.css']
})
export class AddTestDialogComponent implements OnInit {
  myForm : FormGroup;
  category!: Array<DictionaryItemDto>;
  test!: CreateTestRequest;
  constructor(
    public dialogRef: MatDialogRef<AddTestDialogComponent>,
    private formBuilder: FormBuilder,
    private readonly testService: TestService,
    private readonly dictionaryService: DictionaryService,
    iconRegistry: MatIconRegistry,
    sanitizer: DomSanitizer) {
    iconRegistry.addSvgIcon(
      "add",
      sanitizer.bypassSecurityTrustResourceUrl("../../../../../../assets/plus-svgrepo-com.svg")
    );

    iconRegistry.addSvgIcon(
      "remove",
      sanitizer.bypassSecurityTrustResourceUrl("../../../../../../assets/remove-svgrepo-com.svg"));

    this.myForm = formBuilder.group({
      "testName": ["", [Validators.required]],
      "testCategory": ["", [Validators.required]],
      "tasks": formBuilder.array([this.formBuilder.group({
        "description": ["", [Validators.required]],
        "typeId": ["", [Validators.required]],
        "responseOptions": formBuilder.array([[""]]),
        "answers": formBuilder.array([["", Validators.required]]),
        })])
    });
  }

  ngOnInit(): void {
    this.dictionaryService.apiDictionaryGetDictionaryElementsByDictionaryIdGet({id: 1})
      .subscribe(data => {
        if(data && data.content) {
          this.category = data.content;
        }
      });
  }
  onNoClick(): void {
    this.dialogRef.close();
  }

  getFormsControls() : FormArray{
    return this.myForm.controls['tasks'] as FormArray;
  }
  addTask(){
    (<FormArray>this.myForm.controls["tasks"]).push(
      this.formBuilder.group({
        "description": ["", [Validators.required]],
        "typeId": ["", [Validators.required]],
        "responseOptions": this.formBuilder.array([[""]]),
        "answers": this.formBuilder.array([["", Validators.required]]),
      }),
    );
  }

  getFormsOptionsControls(index: number) : FormArray{
    return (<FormArray>this.myForm.controls["tasks"])["controls"][index].get('responseOptions') as FormArray;
  }

  getFormsAnswersControls(index: number) : FormArray{
    return (<FormArray>this.myForm.controls["tasks"])["controls"][index].get('answers') as FormArray;
  }

  addResponseOptions(index: number){
    ((<FormArray>this.myForm.controls["tasks"])["controls"][index].get('responseOptions') as FormArray)
      .push(new FormControl("", Validators.required));
  }

  addAnswers(index: number){
    ((<FormArray>this.myForm.controls["tasks"])["controls"][index].get('answers') as FormArray)
      .push(new FormControl("", Validators.required));
  }

  removeTask(index: number) {
    (<FormArray>this.myForm.controls["tasks"]).removeAt(index);
  }

  removeResponseOptions(groupIndex: number, index: number) {
    ((<FormArray>this.myForm.controls["tasks"])["controls"][groupIndex].get('responseOptions') as FormArray)
      .removeAt(index);
  }
  removeAnswers(groupIndex: number, index: number) {
    (((<FormArray>this.myForm.controls["tasks"])["controls"][groupIndex].get('answers') as FormArray))
      .removeAt(index);
  }

  submit(){
    this.testService.apiTestAddTestPost({body:{
      testName: this.myForm.get('testName')?.value,
        categoryIds: this.myForm.get('testCategory')?.value,
        task: this.myForm.get("tasks")?.value,
      }})
      .subscribe(response =>
      {
        this.dialogRef.close();
      })
  }
}
