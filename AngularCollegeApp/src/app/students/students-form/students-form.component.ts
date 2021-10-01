import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { StudentsService } from '../../service/students.service';
import { City, Country, DialogData, State, Student } from '../../models/student.model';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { SelectionModel } from '@angular/cdk/collections';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-students-form',
  templateUrl: './students-form.component.html',
  styleUrls: ['./students-form.component.css']
})
export class StudentsFormComponent implements OnInit {
  dataSaved = false;
  studentsForm: any;
  allStudents: Observable<Student[]>;
  selection = new SelectionModel<Student>(true, []);
  studentsIdUpdate = null;
  massage = null;
  allCountry: Observable<Country[]>;
  allState: Observable<State[]>;
  allCity: Observable<City[]>;
  CountryId = null;
  StateId = null;
  CityId = null;
  isMale = false;
  isFeMale = false;
  horizontalPosition: MatSnackBarHorizontalPosition = 'center';
  verticalPosition: MatSnackBarVerticalPosition = 'bottom';


  constructor(private formbulider: FormBuilder, 
    private studentsService: StudentsService, 
    private _snackBar: MatSnackBar, 
    public dialog: MatDialog,
    public dialogRef: MatDialogRef<StudentsFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData
    ){}

  onNoClick(): void {
    this.dialogRef.close();
  }


  ngOnInit() {
    this.studentsForm = this.formbulider.group({
      StudentName: ['', [Validators.required]],
      Middlename: ['', [Validators.required]],
      Lastname: ['', [Validators.required]],
      Phone: ['', [Validators.required]],
      Gender: ['', [Validators.required]],
      Address: ['', [Validators.required]],
      City: ['', [Validators.required]],
      State: ['', [Validators.required]],
      Country: ['', [Validators.required]],
      Zipcode: ['', Validators.compose([Validators.required, Validators.pattern('[0-9]{6}')])]
    });
    this.fillCountry();
    if(this.data.Id != null && this.data.Id != undefined){
    this.loadstudentsToEdit(this.data.Id);
    }
  }

  

  onFormSubmit() {
    this.dataSaved = false;
    const students = this.studentsForm.value;
    this.createStudents(students);
    this.studentsForm.reset();
    this.onNoClick();
  }

  loadstudentsToEdit(studentsId: string) {
    this.studentsService.getStudentsById(studentsId).subscribe(Students => {
      this.massage = null;
      this.dataSaved = false;
      this.studentsIdUpdate = Students.Id;
      this.studentsForm.Id = Students.Id;
      this.studentsForm.controls['StudentName'].setValue(Students.StudentName);
      this.studentsForm.controls['Middlename'].setValue(Students.Middlename);
      this.studentsForm.controls['Lastname'].setValue(Students.Lastname);
      this.studentsForm.controls['Phone'].setValue(Students.Phone);
      this.studentsForm.controls['Gender'].setValue(Students.Gender);
      this.studentsForm.controls['Address'].setValue(Students.Address);
      this.studentsForm.controls['Zipcode'].setValue(Students.Zipcode);
      this.studentsForm.controls['Country'].setValue(Students.CountryId);
      this.allState = this.studentsService.getState(Students.CountryId);
      this.CountryId = Students.CountryId;
      this.studentsForm.controls['State'].setValue(Students.StateId);
      this.allCity = this.studentsService.getCity(Students.StateId);
      this.StateId = Students.StateId;
      this.studentsForm.controls['City'].setValue(Students.Cityid);
      this.CityId = Students.Cityid;
      this.isMale = Students.Gender.trim() == "Male" ? true : false;
      this.isFeMale = Students.Gender.trim() == "Female" ? true : false;
    });
  }

  createStudents(students: Student) {
    console.log(students);
    if (this.studentsIdUpdate == null) {
      students.CountryId = this.CountryId;
      students.StateId = this.StateId;
      students.Cityid = this.CityId;

      this.studentsService.createStudent(students).subscribe(() => {
          this.dataSaved = true;
          this.savedSuccessful(1);
          // this.loadAllStudents();
          this.studentsIdUpdate = null;
          this.studentsForm.reset();
        }
      );
    } else {
      students.Id = this.studentsIdUpdate;
      students.CountryId = this.CountryId;
      students.StateId = this.StateId;
      students.Cityid = this.CityId;
      this.studentsService.updateStudent(students).subscribe(() => {
        this.dataSaved = true;
        this.savedSuccessful(0);
        this.studentsIdUpdate = null;
        this.studentsForm.reset();
      });
    }
    this.resetForm();
  }
  deleteStudents(studentsId: string) {
    if (confirm("Are you sure you want to delete this ?")) {
      this.studentsService.deleteStudentById(studentsId).subscribe(() => {
        this.dataSaved = true;
        this.savedSuccessful(2);
        this.studentsIdUpdate = null;
        this.studentsForm.reset();

      });
    }

  }

  fillCountry() {
    this.allCountry = this.studentsService.getCountry();
    this.allState = this.StateId = this.allCity = this.CityId = null;
  }

  fillState(SelCountryId) {
    this.allState = this.studentsService.getState(SelCountryId.value);
    this.CountryId = SelCountryId.value;
    this.allCity = this.CityId = null;
  }

  fillCity(SelStateId) {
    this.allCity = this.studentsService.getCity(SelStateId.value);
    this.StateId = SelStateId.value
  }

  getSelectedCity(City) {
    this.CityId = City.value;
  }

  resetForm() {
    this.studentsForm.reset();
    this.massage = null;
    this.dataSaved = false;
    this.isMale = true;
    this.isFeMale = false;
  }

  savedSuccessful(isUpdate) {
    if (isUpdate == 0) {
      this._snackBar.open('Record Updated Successfully!', 'Close', {
        duration: 2000,
        horizontalPosition: this.horizontalPosition,
        verticalPosition: this.verticalPosition,
      });
    } 
    else if (isUpdate == 1) {
      this._snackBar.open('Record Saved Successfully!', 'Close', {
        duration: 2000,
        horizontalPosition: this.horizontalPosition,
        verticalPosition: this.verticalPosition,
      });
    }
    else if (isUpdate == 2) {
      this._snackBar.open('Record Deleted Successfully!', 'Close', {
        duration: 2000,
        horizontalPosition: this.horizontalPosition,
        verticalPosition: this.verticalPosition,
      });
    }
  }
  


}
