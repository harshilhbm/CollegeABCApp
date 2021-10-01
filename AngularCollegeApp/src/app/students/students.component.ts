import { Component, OnInit, ViewChild } from '@angular/core';
import { Observable } from 'rxjs';
import { StudentsService } from '../service/students.service';
import { Student } from '../models/student.model';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource, } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { SelectionModel } from '@angular/cdk/collections';
import { StudentsFormComponent } from './students-form/students-form.component';

@Component({
  selector: 'app-students',
  templateUrl: './students.component.html',
  styleUrls: ['./students.component.css']
})

export class StudentsComponent implements OnInit {
  allStudents: Observable<Student[]>;
  dataSource: MatTableDataSource<Student>;
  selection = new SelectionModel<Student>(true, []);
  studentsIdUpdate = null;
  horizontalPosition: MatSnackBarHorizontalPosition = 'center';
  verticalPosition: MatSnackBarVerticalPosition = 'bottom';
  displayedColumns: string[] = ['StudentName', 'Middlename', 'Lastname', 'Phone', 'Gender', 'Address', 'City', 'State', 'Country', 'Zipcode', 'Edit', 'Delete'];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private studentsService: StudentsService, 
    private _snackBar: MatSnackBar, 
    public dialog: MatDialog)
    {
    this.studentsService.getAllStudents().subscribe(data => {
      this.dataSource = new MatTableDataSource(data);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }

  ngOnInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  loadAllStudents() {
    this.studentsService.getAllStudents().subscribe(data => {
      this.dataSource = new MatTableDataSource(data);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }

  loadstudentsToEdit(studentsId: string) {
    this.openDialog(studentsId);
  }

  deleteStudents(studentsId: string) {
    if (confirm("Are you sure you want to delete this ?")) {
      this.studentsService.deleteStudentById(studentsId).subscribe(() => {
        this.savedSuccessful();
        this.loadAllStudents();
        this.studentsIdUpdate = null;

      });
    }
  }


  savedSuccessful() {
      this._snackBar.open('Record Deleted Successfully!', 'Close', {
        duration: 2000,
        horizontalPosition: this.horizontalPosition,
        verticalPosition: this.verticalPosition,
      });
  }

  openDialog(Id): void {
    const dialogRef = this.dialog.open(StudentsFormComponent, {
      height: '500px',
      width: '300px',
      data: {Id: Id}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.loadAllStudents();
    });
  }

}
