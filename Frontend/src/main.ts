import 'primevue/resources/themes/saga-blue/theme.css';
import 'primevue/resources/primevue.min.css';
import 'primeicons/primeicons.css';
import 'primeflex/primeflex.css';
import './assets/layout/layout.scss';

import Vue from 'vue';
import Vuex from 'vuex';
import App from './App.vue';
import { router } from './config';
import store from './store';

import { initAxiosSupport } from './config/axios-setup';
import { initVueFilters } from './config/filters-setup';
import { ru } from './config/locale';

import PrimeVue from 'primevue/config';
import AutoComplete from 'primevue/autocomplete';
import Accordion from 'primevue/accordion';
import AccordionTab from 'primevue/accordiontab';
import Avatar from 'primevue/avatar';
import AvatarGroup from 'primevue/avatargroup';
import Badge from 'primevue/badge';
import BadgeDirective from 'primevue/badgedirective';
import Button from 'primevue/button';
import Breadcrumb from 'primevue/breadcrumb';
import Calendar from 'primevue/calendar';
import Card from 'primevue/card';
import Carousel from 'primevue/carousel';
import Checkbox from 'primevue/checkbox';
import Chip from 'primevue/chip';
import Chips from 'primevue/chips';
import ColorPicker from 'primevue/colorpicker';
import Column from 'primevue/column';
import ColumnGroup from "primevue/columngroup/ColumnGroup";
import ConfirmDialog from 'primevue/confirmdialog';
import ConfirmPopup from 'primevue/confirmpopup';
import ConfirmationService from 'primevue/confirmationservice';
import ContextMenu from 'primevue/contextmenu';
import DataTable from 'primevue/datatable';
import DataView from 'primevue/dataview';
import DataViewLayoutOptions from 'primevue/dataviewlayoutoptions';
import Dialog from 'primevue/dialog';
import Divider from 'primevue/divider';
import Dropdown from 'primevue/dropdown';
import Fieldset from 'primevue/fieldset';
import FileUpload from 'primevue/fileupload';
import InlineMessage from 'primevue/inlinemessage';
import Inplace from 'primevue/inplace';
import InputMask from 'primevue/inputmask';
import InputNumber from 'primevue/inputnumber';
import InputSwitch from 'primevue/inputswitch';
import InputText from 'primevue/inputtext';
import Knob from 'primevue/knob';
import Galleria from 'primevue/galleria';
import Listbox from 'primevue/listbox';
import MegaMenu from 'primevue/megamenu';
import Menu from 'primevue/menu';
import Menubar from 'primevue/menubar';
import Message from 'primevue/message';
import MultiSelect from 'primevue/multiselect';
import OrderList from 'primevue/orderlist';
import OverlayPanel from 'primevue/overlaypanel';
import Paginator from 'primevue/paginator';
import Panel from 'primevue/panel';
import PanelMenu from 'primevue/panelmenu';
import ProgressSpinner from 'primevue/progressspinner';
import Password from 'primevue/password';
import PickList from 'primevue/picklist';
import ProgressBar from 'primevue/progressbar';
import Rating from 'primevue/rating';
import RadioButton from 'primevue/radiobutton';
import Ripple from 'primevue/ripple';
import SelectButton from 'primevue/selectbutton';
import ScrollPanel from 'primevue/scrollpanel';
import ScrollTop from 'primevue/scrolltop';
import Slider from 'primevue/slider';
import Sidebar from 'primevue/sidebar';
import Skeleton from 'primevue/skeleton';
import SplitButton from 'primevue/splitbutton';
import Splitter from 'primevue/splitter';
import SplitterPanel from 'primevue/splitterpanel';
import Steps from 'primevue/steps';
import TabMenu from 'primevue/tabmenu';
import Tag from 'primevue/tag';
import TieredMenu from 'primevue/tieredmenu';
import Textarea from 'primevue/textarea';
import Timeline from 'primevue/timeline';
import Toast from 'primevue/toast';
import ToastService from 'primevue/toastservice';
import Toolbar from 'primevue/toolbar';
import TabView from 'primevue/tabview';
import TabPanel from 'primevue/tabpanel';
import Tooltip from 'primevue/tooltip';
import ToggleButton from 'primevue/togglebutton';
import Tree from 'primevue/tree';
import TreeTable from 'primevue/treetable';
import TriStateCheckbox from 'primevue/tristatecheckbox';

import { ValidationProvider, ValidationObserver } from 'vee-validate';

import Workspace from '@/components/shared/Workspace.vue';

import dotenv from 'dotenv';

dotenv.config();

Vue.config.productionTip = false;

Vue.use(PrimeVue, { ripple: true, locale: ru });
Vue.use(ConfirmationService);
Vue.use(ToastService);
Vue.use(Vuex);

initAxiosSupport();
initVueFilters();

Vue.directive('tooltip', Tooltip);
Vue.directive('ripple', Ripple);
Vue.directive('badge', BadgeDirective);

Vue.component('Accordion', Accordion);
Vue.component('AccordionTab', AccordionTab);
Vue.component('AutoComplete', AutoComplete);
Vue.component('Avatar', Avatar);
Vue.component('AvatarGroup', AvatarGroup);
Vue.component('Badge', Badge);
Vue.component('Breadcrumb', Breadcrumb);
Vue.component('Button', Button);
Vue.component('Calendar', Calendar);
Vue.component('Card', Card);
Vue.component('Carousel', Carousel);
Vue.component('Checkbox', Checkbox);
Vue.component('Chip', Chip);
Vue.component('Chips', Chips);
Vue.component('ColorPicker', ColorPicker);
Vue.component('Column', Column);
Vue.component('ColumnGroup', ColumnGroup)
Vue.component('ConfirmDialog', ConfirmDialog);
Vue.component('ConfirmPopup', ConfirmPopup);
Vue.component('ContextMenu', ContextMenu);
Vue.component('DataTable', DataTable);
Vue.component('DataView', DataView);
Vue.component('DataViewLayoutOptions', DataViewLayoutOptions);
Vue.component('Dialog', Dialog);
Vue.component('Divider', Divider);
Vue.component('Dropdown', Dropdown);
Vue.component('Fieldset', Fieldset);
Vue.component('FileUpload', FileUpload);
Vue.component('InlineMessage', InlineMessage);
Vue.component('Inplace', Inplace);
Vue.component('InputMask', InputMask);
Vue.component('InputNumber', InputNumber);
Vue.component('InputSwitch', InputSwitch);
Vue.component('InputText', InputText);
Vue.component('Galleria', Galleria);
Vue.component('Knob', Knob);
Vue.component('Listbox', Listbox);
Vue.component('MegaMenu', MegaMenu);
Vue.component('Menu', Menu);
Vue.component('Menubar', Menubar);
Vue.component('Message', Message);
Vue.component('MultiSelect', MultiSelect);
Vue.component('OrderList', OrderList);
Vue.component('OverlayPanel', OverlayPanel);
Vue.component('Paginator', Paginator);
Vue.component('Panel', Panel);
Vue.component('PanelMenu', PanelMenu);
Vue.component('Password', Password);
Vue.component('PickList', PickList);
Vue.component('ProgressBar', ProgressBar);
Vue.component('ProgressSpinner', ProgressSpinner);
Vue.component('RadioButton', RadioButton);
Vue.component('Rating', Rating);
Vue.component('SelectButton', SelectButton);
Vue.component('ScrollPanel', ScrollPanel);
Vue.component('ScrollTop', ScrollTop);
Vue.component('Slider', Slider);
Vue.component('Sidebar', Sidebar);
Vue.component('Skeleton', Skeleton);
Vue.component('SplitButton', SplitButton);
Vue.component('Splitter', Splitter);
Vue.component('SplitterPanel', SplitterPanel);
Vue.component('Steps', Steps);
Vue.component('TabMenu', TabMenu);
Vue.component('TabView', TabView);
Vue.component('TabPanel', TabPanel);
Vue.component('Tag', Tag);
Vue.component('Textarea', Textarea);
Vue.component('TieredMenu', TieredMenu);
Vue.component('Timeline', Timeline);
Vue.component('Toast', Toast);
Vue.component('Toolbar', Toolbar);
Vue.component('ToggleButton', ToggleButton);
Vue.component('Tree', Tree);
Vue.component('TreeTable', TreeTable);
Vue.component('TriStateCheckbox', TriStateCheckbox);

Vue.component('ValidationProvider', ValidationProvider);
Vue.component('ValidationObserver', ValidationObserver);

Vue.component('Workspace', Workspace);

new Vue({
  router,
  store,
  render: (h) => h(App),
}).$mount('#app');
