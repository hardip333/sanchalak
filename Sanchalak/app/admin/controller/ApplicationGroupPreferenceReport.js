app.controller('ApplicationGroupPreferenceReportCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Application Group Preference Report";

    $scope.cardTitle = "Manage Application Group Preference Report";

    $scope.GroupPreference = {};
    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }

    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }

    $scope.expand_row = function (id) {

        let element = document.getElementById('expand' + id).classList
        if (element.contains("collapse")) {
            document.getElementById("first_col" + id).innerHTML = "-"
            element.remove("collapse")
        } else {
            document.getElementById("first_col" + id).innerHTML = "+"
            element.add("collapse")
        }
    }
    $scope.cancelPendingRequiredDocumentList = function () {
        $scope.GroupPreference = {};
        $scope.IsGroupPreferenceVisible = false;
        $scope.IsExcelButton = false;
        $scope.NoRecLabel = false;
        $scope.IsLabelVisible = false;
    }

    $scope.getFacultyById = function () {

        $http({
            method: 'POST',
            url: 'api/ApplicationGroupPreferenceReport/MstFacultyGetbyId',

            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

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
            url: 'api/ApplicationGroupPreferenceReport/IncAcademicYearGet',
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
        $scope.GroupPreference = { InstituteId: $scope.Institute.InstituteId, AcademicYearId: $scope.GroupPreference.AcademicYearId }
        $http({
            method: 'POST',
            url: 'api/ApplicationGroupPreferenceReport/ProgrammeListGetByInstituteAcademicId',
            data: $scope.GroupPreference,
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
        $scope.GroupPreference = { InstituteId: $scope.Institute.InstituteId, AcademicYearId: $scope.GroupPreference.AcademicYearId, ProgrammeId: $scope.GroupPreference.ProgrammeId }

        $http({
            method: 'POST',
            url: 'api/ApplicationGroupPreferenceReport/IncProgramInstancePartTermGetbyInsIdAcadIdProgId',
            data: $scope.GroupPreference,
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

    $scope.ApplicationGroupPreferenceReportGetByPIPTID = function () {
        if ($scope.GroupPreference.AcademicYearId == null || $scope.GroupPreference.AcademicYearId == undefined || $scope.GroupPreference.AcademicYearId == ""

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
        else if ($scope.GroupPreference.ProgrammeId == null || $scope.GroupPreference.ProgrammeId == undefined || $scope.GroupPreference.ProgrammeId == ""

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
        else if ($scope.GroupPreference.ProgrammeInstancePartTermId == null || $scope.GroupPreference.ProgrammeInstancePartTermId == undefined || $scope.GroupPreference.ProgrammeInstancePartTermId == ""

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
                url: 'api/ApplicationGroupPreferenceReport/GroupPreferenceReportGetByProgInstPartTermId',
                data: $scope.GroupPreference,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        $scope.NoRecLabel = true;
                        $scope.IsLabelVisible = false;
                        $scope.IsGroupPreferenceVisible = false;
                        $scope.offSpinner();
                    }

                    else {
                        $scope.offSpinner();
                        $scope.GroupPreferenceDoc = response.obj;
                        for (let i = 0; i < $scope.GroupPreferenceDoc.length; i++) {
                            $scope.ProgramLbl = {};
                            $scope.ProgramLbl.FacultyName = $scope.GroupPreferenceDoc[i].FacultyName;
                            $scope.ProgramLbl.AcademicYearCode = $scope.GroupPreferenceDoc[i].AcademicYearCode;
                            $scope.ProgramLbl.ProgrammeName = $scope.GroupPreferenceDoc[i].ProgrammeName;                   
                            $scope.ProgramLbl.BranchName = $scope.GroupPreferenceDoc[i].BranchName;                        
                            $scope.ProgramLbl.InstancePartTermName = $scope.GroupPreferenceDoc[i].InstancePartTermName;

                        }
                        $scope.IsGroupPreferenceVisible = true;
                        $scope.IsLabelVisible = true;
                        $scope.NoRecLabel = false;
                       
                        $scope.GroupPreferenceTableParams = new NgTableParams({
                        }, {
                            dataset: response.obj
                        });


                   




                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });

        }
        
    };


    $scope.ExportGroupPreferenceReportInExcel = function () {
        alert("Please wait, Excel is being prepared...");
        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();
        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "ApplicantGroupPreferenceReport_" + DateWithoutDashed + time;
        var mystyle = {
            headers: true,
           //style: 'font-size:25px;font-weight:bold',
            caption: {
                title: '<h2 style=font-size: 25px; text-align:left; font- weight:bold>Applicant Group Preference Report | Date and Time: ' + DateAndTime+
                    '<h3 style=text-align:left;font-weight:bold> Faculty Name: ' + $scope.GroupPreferenceDoc[0].FacultyName +
                    '<br>Academic Year:' + $scope.GroupPreferenceDoc[0].AcademicYearCode +
                    '<br>Programme Name: ' + $scope.GroupPreferenceDoc[0].ProgrammeName +
                    '<br>Branch Name:' + $scope.GroupPreferenceDoc[0].BranchName +
                    '<br>Instance Part Term Name:' + $scope.GroupPreferenceDoc[0].InstancePartTermName + '</h3>',
            },
            
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'ApplicationNo', title: 'Application Number' },
                { columnid: 'UserName', title: 'Applicant User Name' },
                { columnid: 'NameAsPerMarksheet', title: 'Applicant Name' },
                { columnid: 'EmailId', title: 'EmailId' },
                { columnid: 'MobileNo', title: 'Mobile No' },
                { columnid: 'SocialCategoryName', title: 'Social Category Name' },
                { columnid: 'EligibleDegreeName', title: 'Last Qualifying Degree Name' },
                { columnid: 'Percentage', title: 'Last Qualifying Percentage' },
                { columnid: 'Choice1', title: 'Choice1' },
                { columnid: 'Choice2', title: 'Choice2' },
                { columnid: 'Choice3', title: 'Choice3' },
                { columnid: 'Choice4', title: 'Choice4' },
                { columnid: 'Choice5', title: 'Choice5' },
                { columnid: 'Choice6', title: 'Choice6' },
                { columnid: 'Choice7', title: 'Choice7' },
                { columnid: 'Choice8', title: 'Choice8' },
                { columnid: 'Choice9', title: 'Choice9' },
                { columnid: 'Choice10', title: 'Choice10' },
                { columnid: 'Choice11', title: 'Choice11' },
                { columnid: 'Choice12', title: 'Choice12' },
                { columnid: 'Choice13', title: 'Choice13' },
                { columnid: 'Choice14', title: 'Choice14' },
                { columnid: 'Choice15', title: 'Choice15' },
                { columnid: 'Choice16', title: 'Choice16' },
                { columnid: 'Choice17', title: 'Choice17' },
                { columnid: 'Choice18', title: 'Choice18' },
                { columnid: 'Choice19', title: 'Choice19' },
                { columnid: 'Choice20', title: 'Choice20' },
                { columnid: 'Choice21', title: 'Choice21' },
                { columnid: 'Choice22', title: 'Choice22' },
                { columnid: 'Choice23', title: 'Choice23' },
                { columnid: 'Choice24', title: 'Choice24' },
                { columnid: 'Choice25', title: 'Choice25' },
                { columnid: 'Choice26', title: 'Choice26' },
                { columnid: 'Choice27', title: 'Choice27' },
                { columnid: 'Choice28', title: 'Choice28' },
                { columnid: 'Choice29', title: 'Choice29' },
                { columnid: 'Choice30', title: 'Choice30' },
                { columnid: 'Choice31', title: 'Choice31' },
              


            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.GroupPreferenceDoc]);
    };





    


});