import { Component, OnInit, Input } from '@angular/core';
import { SharedServiceService } from 'src/app/shared-service.service';

@Component({
  selector: 'app-add-edit-emp',
  templateUrl: './add-edit-emp.component.html',
  styleUrls: ['./add-edit-emp.component.css']
})
export class AddEditEmpComponent implements OnInit {
  @Input() emp: any;
  EmpoyeeId!: number;
  EmployeeName!: string;
  Department!: string;
  DateOfJoining!: string;
  Photo!: string;
  PhotoFilePath!: string;

  DepartmentList: any = [];

  constructor(private service: SharedServiceService) { }

  ngOnInit(): void {
    this.LoadDepartmentList();
  }

  LoadDepartmentList() {
    this.service.getAllDeptNames().subscribe((data: any) => {
      this.DepartmentList = data;

      this.EmpoyeeId = this.emp.EmpoyeeId;
      this.EmployeeName = this.emp.EmployeeName;
      this.Department = this.emp.Department;
      this.DateOfJoining = this.emp.DateOfJoining;
      this.Photo = this.emp.Photo;
      this.PhotoFilePath = this.service.photoURL + this.Photo;
    });
  }

  addEmployee() {
    var val = {
      EmpoyeeId: this.EmpoyeeId,
      EmployeeName: this.EmployeeName,
      Department: this.Department,
      DateOfJoining: this.DateOfJoining,
      Photo: this.Photo
    };
    this.service.addEmployee(val).subscribe(res => {
      alert(res.toString);
    });



  }
  updateDepartment() {
    var val = { 
      EmpoyeeId: this.EmpoyeeId,
      EmployeeName: this.EmployeeName,
      Department: this.Department,
      DateOfJoining: this.DateOfJoining,
      Photo: this.Photo 
      };
    this.service.updateEmployee(val).subscribe(res => {
      alert(res.toString);
    });



  }

  uploadPhoto(event:any){
    var file=event.target.files[0];
    const formData:FormData=new FormData();
    formData.append('Uploaded iFile',file,file.name);
    this.service.SavePhoto(formData).subscribe((data:any)=>{
      this.Photo=data.toString();
      this.PhotoFilePath=this.service.photoURL+this.Photo;
    });

  }

}
