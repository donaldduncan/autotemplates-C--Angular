import { Component, OnInit, OnChanges } from '@angular/core';
import { FormBuilder, NgForm, FormGroup } from '@angular/forms';
import { TrellobotService, Trellobot } from '../trellobot.service';

@Component({
  selector: 'app-bot-setup',
  templateUrl: './bot-setup.component.html',
  styleUrls: ['./bot-setup.component.css']
})
export class BotSetupComponent implements OnInit {

  trellobotform = this.fb.group({
    /*ID: [],*/
    name: [''],
    /*appKey: [''],*/
    /*token: [''],*/
    boardId: [''],
    templateId: ['']
  });

  private trellobots: Trellobot[];
  private members: any;

  constructor(private trellobotService: TrellobotService, private fb: FormBuilder) { }

  ngOnInit() {
    this.getTrellobots();
    this.trellobotform.reset();
  }

  private getTrellobots(): void {
    this.trellobotService.getAllTrellobots().subscribe(trellobots => {
      this.trellobots = trellobots;
      this.getMembers();
    });
  }

/*   private getMembers(): void {
    this.trellobotService.getMembers(this.trellobots).subscribe(members => {this.members = members;
      console.log(this.members);
    }); */

  private getMembers(): void {
    const members = new Set<string>(this.trellobots.map(el => el.MemberId));
    console.log('Members:', members);
    members.forEach(el => {
      this.trellobotService.getMembers_TrelloApi(el).subscribe(member => {this.members.push(member);
      });
        console.log(this.members);
    });
  }

  onClickDelete(id: number) {
    this.trellobotService.deleteTrellobot(id)
      .subscribe(trellobot => this.trellobots = trellobot);
  }

  onClickTableRow(trellobot: Trellobot) {
    console.log(trellobot);
    this.trellobotform.patchValue(trellobot);
    console.log(this.trellobotform);
  }

  public onSubmit() {
    console.log(this.trellobotform.value);
    if (this.trellobotform.value.id === null) {
      this.trellobotService.addTrellobot(this.trellobotform.value).subscribe(trellobot => this.trellobots.push(trellobot));
      this.trellobotform.reset();
    } else {
      this.trellobotService.updateTrellobot(this.trellobotform.value).subscribe();
    }
  }
}
