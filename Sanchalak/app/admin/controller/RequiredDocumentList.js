 app.controller('RequiredDocumentListCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Required Document List";

    $scope.cardTitle = "Required Document List";

     $scope.RequiredDoc = {};

     $scope.cancelRequiredDocumentReport = function () {
        
         $scope.RequiredDoc = {};
         $scope.IsTableVisible = false;
         $scope.IsExcelButton = false;
         $scope.IsLabelVisible = false;
         $scope.NoRecLabel = false;
    }

     //Spinner ON
     $scope.onSpinner = function on() {
         document.getElementById("overlay").style.display = "flex";
     }
     //Spinner OFF
     $scope.offSpinner = function off() {
         document.getElementById("overlay").style.display = "none";
     }
   
     $scope.getFacultyById = function () {
        
        $http({
            method: 'POST',
            url: 'api/RequiredDocumentList/MstFacultyGetbyId',

            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                // $scope.Faculty = response.obj[0]; 
                $scope.Institute = response.obj[0];    
               
                
            })
            .error(function (res) {
                alert(res);
            });
     };

     $scope.IncAcademicYearListGet = function () {
         $scope.AcademicYearList = {};
         $http({
             method: 'POST',
             url: 'api/RequiredDocumentList/IncAcademicYearGet',
             headers: { "Content-Type": 'application/json' }
         })

             .success(function (response) {
                 $scope.AcademicYearList = response.obj;

             })
             .error(function (res) {

             });
     };

     $scope.getProgrammeListByInstIdAcadId = function () {
         $scope.ProgrammeList = {};
         $scope.RequiredDoc = { InstituteId: $scope.Institute.InstituteId, AcademicYearId: $scope.RequiredDoc.AcademicYearId }
         $http({
             method: 'POST',
             url: 'api/RequiredDocumentList/ProgrammeListGetByInstituteAcademicId',
             data: $scope.RequiredDoc,
             headers: { "Content-Type": 'application/json' }
         })
             .success(function (response) {
                 $scope.ProgrammeList = response.obj;
             })
             .error(function (res) {
                 alert(res);
                 $scope.ProgrammeList = {};
             });
     };

     $scope.getInstanceNameList = function () {
         $scope.InstanceNameList = {};
         $scope.RequiredDoc = { InstituteId: $scope.Institute.InstituteId, AcademicYearId: $scope.RequiredDoc.AcademicYearId, ProgrammeId: $scope.RequiredDoc.ProgrammeId }

         $http({
             method: 'POST',
             url: 'api/RequiredDocumentList/IncProgramInstancePartTermGetbyInsIdAcadIdProgId',
             data: $scope.RequiredDoc,
             headers: { "Content-Type": 'application/json' }
         })
             .success(function (response) {
                 if (response.response_code == "0") {
                     $state.go('login');

                 } else if (response.response_code == "201") {
                     $rootScope.broadcast('dialog', "Error", "alert", response.obj);
                     $scope.InstanceNameList = {};
                 }
                 else {

                     $scope.InstanceNameList = response.obj;
                 }
             })
             .error(function (res) {
                 $rootScope.broadcast('dialog', "Error", "alert", res.obj);
             });
     };
  
   
     $scope.getRequiredDocumentReport = function () {
  
         if ($scope.RequiredDoc.AcademicYearId == null || $scope.RequiredDoc.AcademicYearId == undefined || $scope.RequiredDoc.AcademicYearId == ""

         ) {

             $mdDialog.show(
                 $mdDialog.alert()
                     .parent(angular.element(document.querySelector('#popupContainer')))
                     .clickOutsideToClose(true)
                     .title("Error")
                     .textContent("Kindly Select Academic Year DropDown Value")
                     .ariaLabel('Alert Dialog Demo')
                     .ok('Okay!')
             );
         }
         else if ($scope.RequiredDoc.ProgrammeId == null || $scope.RequiredDoc.ProgrammeId == undefined || $scope.RequiredDoc.ProgrammeId == ""

         ) {

             $mdDialog.show(
                 $mdDialog.alert()
                     .parent(angular.element(document.querySelector('#popupContainer')))
                     .clickOutsideToClose(true)
                     .title("Error")
                     .textContent("Kindly Select Programme DropDown Value")
                     .ariaLabel('Alert Dialog Demo')
                     .ok('Okay!')
             );
         }
         else if ($scope.RequiredDoc.ProgrammeInstancePartTermId == null || $scope.RequiredDoc.ProgrammeInstancePartTermId == undefined || $scope.RequiredDoc.ProgrammeInstancePartTermId == ""

         ) {

             $mdDialog.show(
                 $mdDialog.alert()
                     .parent(angular.element(document.querySelector('#popupContainer')))
                     .clickOutsideToClose(true)
                     .title("Error")
                     .textContent("Kindly Select Programme Instance Part Term DropDown Value")
                     .ariaLabel('Alert Dialog Demo')
                     .ok('Okay!')
             );
         }
         else {
             $scope.onSpinner();
             $http({
                 method: 'POST',
                 url: 'api/RequiredDocumentList/RequiredDocumentExportExcel',
                 data: $scope.RequiredDoc,
                 headers: { "Content-Type": 'application/json' }
             })

                 .success(function (response) {
                     $rootScope.showLoading = false;

                     if (response.response_code == "0") {
                         $state.go('login');

                     } else if (response.response_code != "200") {

                         $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                         $scope.offSpinner();
                         $scope.IsLabelVisible = false;
                         $scope.NoRecLabel = true;
                         $scope.IsTableVisible = false;
                         $scope.IsExcelButton = false;

                     }

                     else {

                         $scope.offSpinner();
                       
                         $scope.RequiredDocumentReport = response.obj;
                         for (let i = 0; i < $scope.RequiredDocumentReport.length; i++) {
                             
                             $scope.ProgramLbl = {};
                             $scope.ProgramLbl.ProgrammeName = $scope.RequiredDocumentReport[i].ProgrammeName;
                             $scope.ProgramLbl.InstancePartTermName = $scope.RequiredDocumentReport[i].InstancePartTermName;
                         }
                         $scope.IsLabelVisible = true;
                         $scope.IsTableVisible = true;
                         $scope.IsExcelButton = true;
                         $scope.NoRecLabel = false;
                         $scope.RequiredDocumentReportTableParams = new NgTableParams({
                         }, {
                             dataset: response.obj
                         })

                     }

                 })

                 .error(function (res) {


                     $rootScope.$broadcast('dialog', "Error", "alert", res.obj);

                 });
         }
     };

     $scope.exportData = function () {

         var LongDate = new Date($.now());
         var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
         var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
         var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();
         var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
         var ExcelFileName = "Required Document Report" + DateWithoutDashed + time;
         var mystyle = {
             headers: true,
             style: 'font-size:20px;font-weight:bold',
             caption: {
                 title: 'Required Document Report|Date and Time: ' + '<br>' + DateAndTime,
             },
             columns: [
                 { columnid: 'IndexId', title: 'Sr.No' },
                 { columnid: 'ProgrammeName', title: 'Programme Name' },
                 { columnid: 'InstancePartTermName', title: 'Instance Part Term Name' },
                 { columnid: 'NameOfTheDocument', title: 'Name Of The Document' },
                 { columnid: 'DocumentType', title: 'Document Type' },
                
                 
             ],
         };

         //Create XLS format using alasql.js file.
         alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.RequiredDocumentReport]);
     };

});