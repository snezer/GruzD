<template>
  <div>
    <div class="acceptance-wrapper">
      <div class="acceptance-control">
        <Control :visible-control="false"/>
      </div>
      <div class="acceptance-events">
        <div class="p-grid">
          <div class="p-col-12">
            <DataTable :value="listEvents" :selection.sync="selectedEvent" selectionMode="single" dataKey="Occured" :row-class="rowClass"
                       :paginator="true" :rows="5"
                       paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink">
              <Column field="ClassifiedType" header="Тип события">
                <template #body="slotProps">
                  {{eventTypeWord[slotProps.data.ClassifiedType]}}
                </template>
              </Column>
              <Column field="Occured" header="Время события">
                <template #body="slotProps">
                  {{calcDate(slotProps.data.Occured)}}
                </template>
              </Column>
              <Column field="Weight" header="Вес"></Column>
            </DataTable>
          </div>
          <div class="p-col-12">
            <Acceptance :event="selectedEvent"/>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import {Component, Vue} from 'vue-property-decorator';
import Acceptance from '@/pages/acceptance/Acceptance.vue';
import Control from "@/pages/conrol/Control.vue";
import { EventTypeWord } from "@/models/enum/EventType";
@Component({
  components: { Control, Acceptance },
})
export default class Acceptances extends Vue {
  components: { Acceptance };
  selectedEvent:Acceptance | null = null;
  expandedRowGroups = null;
  eventTypeWord = EventTypeWord

  get selectEvent() {
    return JSON.parse(JSON.stringify(this.selectedEvent));
  }
  get listEvents() {
    return this.$store.state.zone.eventListActiveZoneState
  }
  calcDate(date){
    return new Date(date).toLocaleDateString('ru-Ru', {year: "numeric", month: "2-digit", day: "2-digit", hour: "2-digit", minute: "2-digit", second: "2-digit"})
  }
  rowClass(data){
    return !(data.ClassifiedType>=4 || data.ClassifiedType<=6) ? 'row-accessories':null
  }
  created(){
    this.selectedEvent = this.listEvents[0]
  }
}
</script>

<style>
.acceptance-wrapper{
  display: grid;
  grid-template-columns: 7fr 5fr;
}
.p-datatable .p-datatable-tbody > tr{
  background: unset;
  color: unset;
  outline: unset;
}
.p-datatable.p-datatable-hoverable-rows .p-datatable-tbody > tr:not(.p-highlight):hover{
  background: #372235;
  color: unset;
}
.p-datatable .p-datatable-tbody > tr.p-highlight{
  background: #372235;
  color: unset;
}
.p-datatable .p-datatable-tbody > tr > td{
  border: unset
}
.gruzd thead{
  display: none;
}
.p-rowgroup-header{
  border-bottom: 1px #322F43 solid;

}
.caption{
  height:41px;
  background-color: rgba(255,255,255,.1)
}
.row-accessories {
  background-color: rgba(238, 76, 76, 0.4) !important;

}

.p-paginator{
  background-color: #372235;
  border: none;
}
</style>
