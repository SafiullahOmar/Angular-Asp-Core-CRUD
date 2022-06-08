import { Component, OnInit } from '@angular/core';
import { SharedServiceService } from 'src/app/shared-service.service';

@Component({
  selector: 'app-show-emp',
  templateUrl: './show-emp.component.html',
  styleUrls: ['./show-emp.component.css']
})
export class ShowEmpComponent implements OnInit {

  myEmpList: any = [];
  ModalTitle!: string;
  emp: any;
  ActiveAddEditEmpComp: boolean = false;

  constructor(private service: SharedServiceService) { }

  ngOnInit(): void {
    this.refreshEmpList();
  }

  refreshEmpList() {
    return this.service.getEmployeeList().subscribe(data => {
      this.myEmpList = data;
    });
  }

  addEmp() {
    this.ActiveAddEditEmpComp = true;
    this.ModalTitle = "Add Department";
    this.emp = {
      EmpoyeeId: 0,
      EmployeeName: "",
      Department:"",
      DateOfJoining:"",
      Photo:"anonymous.png"

    }

    this.refreshEmpList();
  }
  editEmp(empDetail:any) {
    this.emp = empDetail;
    this.ModalTitle="Edit Modal";
    this.ActiveAddEditEmpComp=true;
  }
  deleteEmp(empDetail:any) {
    if(confirm('Are u Sure to delete this record ?')){
    
      this.service.deleteEmployee(empDetail.EmpoyeeId).subscribe(data=>{
         alert(data.toString());
         this.refreshEmpList(); 
      });
    }
  }
  close() {

    this.ActiveAddEditEmpComp = false;
    this.refreshEmpList();
  }


}

