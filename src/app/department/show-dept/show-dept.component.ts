import { Component, OnInit } from '@angular/core';
import { SharedServiceService } from 'src/app/shared-service.service';
import { } from 'rxjs';

@Component({
  selector: 'app-show-dept',
  templateUrl: './show-dept.component.html',
  styleUrls: ['./show-dept.component.css']
})
export class ShowDeptComponent implements OnInit {

  myDeptList: any = [];
  ModalTitle!: string;
  dept: any;
  ActiveAddEditDepComp: boolean = false;

  constructor(private service: SharedServiceService) { }

  ngOnInit(): void {
    this.refreshDeptList();
  }

  refreshDeptList() {
    return this.service.getDepartmentList().subscribe(data => {
      this.myDeptList = data;
    });
  }

  addDept() {
    this.ActiveAddEditDepComp = true;
    this.ModalTitle = "Add Department";
    this.dept = {
      DepartmentId: 0,
      DepartmentName: ""
    }

    this.refreshDeptList();
  }
  editDept(deptDetail:any) {
    this.dept = deptDetail;
    this.ModalTitle="Edit Modal";
    this.ActiveAddEditDepComp=true;
  }
  deleteDept(deptDetail:any) {
    if(confirm('Are u Sure to delete this record ?')){
      alert(deptDetail.DepartmentId);
      this.service.deleteDepartment(deptDetail.DepartmentId).subscribe(data=>{
         alert(data.toString());
         this.refreshDeptList(); 
      });
    }
  }
  close() {

    this.ActiveAddEditDepComp = false;
    this.refreshDeptList();
  }


}
