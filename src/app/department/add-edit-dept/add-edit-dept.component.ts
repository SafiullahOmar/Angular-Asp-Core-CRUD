import { Component, OnInit,Input } from '@angular/core';
import{SharedServiceService} from 'src/app/shared-service.service';
@Component({
  selector: 'app-add-edit-dept',
  templateUrl: './add-edit-dept.component.html',
  styleUrls: ['./add-edit-dept.component.css']
})
export class AddEditDeptComponent implements OnInit {
@Input() dept:any;
DepartmentId!:number;
DepartmentName!:string;

  constructor(private service:SharedServiceService) { }

  ngOnInit(): void {
    this.DepartmentId=this.dept.DepartmentId;
    this.DepartmentName=this.dept.DepartmentName;
  }

  addDepartment(){
    var val={DepartmentId:this.DepartmentId,DepartmentName:this.DepartmentName};
    this.service.addDepartment(val).subscribe(res=>{
        alert(res.toString);
    });

    

  }
  updateDepartment(){
    var val={DepartmentId:this.DepartmentId,DepartmentName:this.DepartmentName};
    this.service.updateDepartment(val).subscribe(res=>{
        alert(res.toString);
    });

  }

}
