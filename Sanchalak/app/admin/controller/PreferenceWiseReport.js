app.controller('PreferenceWiseReportCtrl', function ($scope, $http, $filter, $rootScope, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {
    $rootScope.pageTitle = "Preference Wise Report";
    $scope.resetPreferenceWiseReport = function () {
        $scope.PreferenceWiseReport = {};
    }
    $scope.StudentWiseTable = false;

    $scope.IncAcademicYearListGet = function () {

        $http({
            method: 'Post',
            url: 'api/PreferenceWiseReport/IncAcadYearCode',

            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $scope.AcademicList = response.obj;




            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getFacultyById = function () {
        //debugger;
        $scope.FacultyList = "";
        $http({
            method: 'POST',
            url: 'api/PreferenceWiseReport/FacultyGetById',
            data: { AcademicYearId: $scope.PreferenceWiseReport.AcademicYearId, Id: $cookies.get('InstituteId') },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $scope.FacultyList = response.obj;
                $scope.ProgrammeList = "";
                $scope.ProgPartList = "";
                $scope.BList = "";
                $scope.PPTList = "";
                $scope.PGList = "";
                $scope.PNList = "";

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.ProgrammeGetbyFacultyId = function () {
        var FacId = { FacultyId: $scope.PreferenceWiseReport.FacultyId }
        $http({
            method: 'Post',
            url: 'api/PreferenceWiseReport/ProgrammeGetByFacId',
            data: FacId,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $scope.ProgrammeList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.GetProgrammePart = function () {
        var ProgId = { ProgrammeId: $scope.PreferenceWiseReport.ProgrammeId, FacultyId: $scope.PreferenceWiseReport.FacultyId }
        //alert(ProgId);
        $http({
            method: 'Post',
            url: 'api/PreferenceWiseReport/ProgrammePartGetByFIdPID',
            data: ProgId,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $scope.ProgPartList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.BranchGet = function () {

        $http({
            method: 'Post',
            url: 'api/PreferenceWiseReport/MstSpecialisationGetFIDPIDPPIDGet',
            data: {
                FacultyId: $scope.PreferenceWiseReport.FacultyId,
                ProgrammeId: $scope.PreferenceWiseReport.ProgrammeId,
                ProgrammePartId: $scope.PreferenceWiseReport.ProgrammePartId,
            },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $scope.BList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.GetProgrammePartTerm = function () {

        $http({
            method: 'Post',
            url: 'api/PreferenceWiseReport/MstProgrammePartTermGetByFIDPIDPPIDSIDGet',
            data: {
                FacultyId: $scope.PreferenceWiseReport.FacultyId,
                ProgrammeId: $scope.PreferenceWiseReport.ProgrammeId,
                ProgrammePartId: $scope.PreferenceWiseReport.ProgrammePartId,
                SpecialisationId: $scope.PreferenceWiseReport.SpecialisationId
            },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $scope.PPTList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };

    //$scope.GetIncProgrammeInstPartTerm = function () {

    //    $http({
    //        method: 'Post',
    //        url: 'api/PreferenceWiseReport/IncProgInstPartTermGetByFIDPIDPPIDSIDGet',
    //        data: {
    //            FacultyId: $scope.PreferenceWiseReport.FacultyId,
    //            ProgrammeId: $scope.PreferenceWiseReport.ProgrammeId,
    //            ProgrammePartId: $scope.PreferenceWiseReport.ProgrammePartId,
    //            SpecialisationId: $scope.PreferenceWiseReport.SpecialisationId,
    //            ProgrammePartTermId: $scope.PreferenceWiseReport.ProgrammePartTermId
    //        },
    //        headers: { "Content-Type": 'application/json' }
    //    })
    //        .success(function (response) {

    //            $scope.InstPPTList = response.obj;
    //        })
    //        .error(function (res) {
    //            alert(res);
    //        });
    //};


    $scope.GetPreferenceGroup = function () {

        ObjPref = {
            AcademicYearId: $scope.PreferenceWiseReport.AcademicYearId,
            FacultyId: $scope.PreferenceWiseReport.FacultyId,
            SpecialisationId: $scope.PreferenceWiseReport.SpecialisationId,
            ProgrammeId: $scope.PreferenceWiseReport.ProgrammeId,
            ProgrammePartId: $scope.PreferenceWiseReport.ProgrammePartId,
            ProgrammePartTermId: $scope.PreferenceWiseReport.ProgrammePartTermId
        }

        $http({
            method: 'Post',
            url: 'api/PreferenceWiseReport/MstPreferenceGroupGetByAYIDFIDPIDPPIDSPIDGet',
            data: ObjPref,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $scope.PGList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };


    $scope.GetPreferenceNo = function () {

        ObjPrefNo = {
            AcademicYearId: $scope.PreferenceWiseReport.AcademicYearId,
            FacultyId: $scope.PreferenceWiseReport.FacultyId,
            SpecialisationId: $scope.PreferenceWiseReport.SpecialisationId,
            ProgrammeId: $scope.PreferenceWiseReport.ProgrammeId,
            ProgrammePartId: $scope.PreferenceWiseReport.ProgrammePartId,
            ProgrammePartTermId: $scope.PreferenceWiseReport.ProgrammePartTermId,
            PreferenceId: $scope.PreferenceWiseReport.PreferenceGroupId
        }

        $http({
            method: 'Post',
            url: 'api/PreferenceWiseReport/MstPreferenceNumberGetByFacIdGet',
            data: ObjPrefNo,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $scope.PNList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };


    $scope.getPreferenceWiseReport = function (PreferenceNo) {
        //debugger
        var ObjPreferenceGroup = {
            AcademicYearId: $scope.PreferenceWiseReport.AcademicYearId,
            FacultyId: $scope.PreferenceWiseReport.FacultyId,
            ProgrammeId: $scope.PreferenceWiseReport.ProgrammeId,
            ProgrammePartId: $scope.PreferenceWiseReport.ProgrammePartId,
            SpecialisationId: $scope.PreferenceWiseReport.SpecialisationId,
            ProgrammePartTermId: $scope.PreferenceWiseReport.ProgrammePartTermId,
            PreferenceGroupId: $scope.PreferenceWiseReport.PreferenceGroupId,
            PreferenceNo: PreferenceNo,
            EligibleDegreeId: 2,
        };
        //console.log(Obj)
        $http({
            method: 'POST',
            url: 'api/PreferenceWiseReport/PreferenceWiseReportGet',
            data: ObjPreferenceGroup,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                //debugger
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    //console.log(response.obj);
                    if (response.obj === "No Record Found") {

                        $scope.NoRecordFound = true;
                        $scope.PreferenceWiseReportTableParams = new NgTableParams({
                        }, {
                            dataset: [],
                        });
                    }
                    else {
                        $scope.PreferenceWiseReportTableParams = new NgTableParams({},
                            { dataset: response.obj });
                        $scope.PreferenceReport = response.obj;
                        $scope.PWRD = response.obj[0];
                        $scope.ExcelPreferenceWiseReport = response.obj;
                        $scope.StudentWiseTable = true;
                    }
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };


    $scope.exportPreferenceWiseReport = function () {

        if ($scope.ExcelPreferenceWiseReport == undefined) {

            alert("Please select Programme Instance Part Term then click on Search");
            return false;
        }
        //alert("Please wait, Excel is being prepared...Do not click again on Export to Excel button until Excel is download");
        //debugger
        var FacultyName = $scope.PWRD.FacultyName;
        var PreferenceName = $scope.PWRD.PreferenceName;
        var PreferenceNo = $scope.PWRD.PreferenceNo;
        var ProgrammeName = $scope.PWRD.ProgrammeName;
        var PartTermName = $scope.PWRD.PartTermName;
        var AcademicYearCode = $scope.PWRD.AcademicYearCode;
        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "PreferenceWiseReport_" + DateWithoutDashed + time;

        var mystyleSecond = {
            headers: true,
            style: 'font-size:20px;font-weight:bold;letter-spacing: 3px;',
            caption: {
                title: 'AcademicYearCode: ' + AcademicYearCode + 'FacultyName: ' + FacultyName + '<br>' +
                    'Programme Name: ' + ProgrammeName + 'Part Term Name: ' + PartTermName +
                    'Choice Number: ' + PreferenceNo + '<br>' + ' Preference Name: ' + PreferenceName + '<br>' +
                    'Preference Wise Report | Date and Time: ' + DateAndTime,

            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'ApplicationNo', title: 'ApplicationNo' },
                { columnid: 'ApplicantUserName', title: 'ApplicantUserName' },
                { columnid: 'NameAsPerMarksheet', title: 'Full Name' },
                { columnid: 'MobileNo', title: 'Mobile No' },
                { columnid: 'EmailId', title: 'Email Id' },
                { columnid: 'MarkObtained', title: 'Mark Obtained' },
                { columnid: 'MarkOutof', title: 'Mark Outof' },
                { columnid: 'Percentage', title: 'Percentage' },
                { columnid: 'IsPhysicallyChanllenged', title: 'Physically Chanllenged' },
                { columnid: 'ApplicationReservationName', title: 'Category' },
                { columnid: 'EWSCategory', title: 'EWS' },
                { columnid: 'Location', title: 'Location' },

            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyleSecond, $scope.ExcelPreferenceWiseReport]);
    };





})