app.controller('PreExamDataReportCtrl', function ($scope, $http,$filter, $rootScope, Upload, $localStorage, $state, $cookies, $location, $mdDialog, NgTableParams, $timeout) {

    $scope.PreExam = {};
    $scope.showTimeTable = false;
    $scope.ShowLabel = false;
    
    $scope.getExamEvent = function () {

        $http({
            method: 'POST',
            url: 'api/PreExamDataReport/ExamEventGetForDropDown',            
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ExamMasterListGetActiveList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getInstituteByExamEvent = function () {
        $scope.InstituteList = {};
        data = { ExamMasterId: $scope.PreExam.ExamMasterId };

        $http({
            method: 'POST',
            url: 'api/PreExamDataReport/MstInstituteGetByExamEvent',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.InstituteList = response.obj;
                
            })
            .error(function (res) {
                alert(res);
            });
    };    

    $scope.getProgrammeByFacultyExamId = function () {
        $scope.ProgrammeList = {};
        data = { FacultyExamMapId: $scope.PreExam.FacultyExamId };

        $http({
            method: 'POST',
            url: 'api/PreExamDataReport/MstProgrammeGetByFacultyExamId',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgrammeList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getBranchByProgrammeId = function () {
        $scope.BranchList = {};
        data = { ProgrammeId: $scope.PreExam.ProgrammeId };

        $http({
            method: 'POST',
            url: 'api/PreExamDataReport/MstProgrammeBranchListGetByProgrammeId',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.BranchList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getProgrammePartTermByProgrammeId = function () {
        $scope.ProgrammePartTermList = {};
        data = { ProgrammeId: $scope.PreExam.ProgrammeId };

        $http({
            method: 'POST',
            url: 'api/PreExamDataReport/MstProgrammePartTermGetbyProgrammeId',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgrammePartTermList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.submit = function () {
        
        if ($scope.PreExam.ExamMasterId == null || $scope.PreExam.ExamMasterId == undefined || $scope.PreExam.ExamMasterId == "" ||      
            $scope.PreExam.SpecialisationId == null || $scope.PreExam.SpecialisationId == undefined || $scope.PreExam.SpecialisationId == "" ||
            $scope.PreExam.ProgrammePartTermId == null || $scope.PreExam.ProgrammePartTermId == undefined || $scope.PreExam.ProgrammePartTermId == "")
        {
            alert("Please Select All Fields Before Click Submit !!!");
            $scope.showTimeTable = false;
        }

        else {

            var data = {ExamMasterId : $scope.PreExam.ExamMasterId,
                BranchId: $scope.PreExam.SpecialisationId,
                ProgrammePartTermId: $scope.PreExam.ProgrammePartTermId                
            };

            $http({
                method: 'POST',
                url: 'api/PreExamDataReport/PreExamDataGetForReport',
                data: data,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {

                    $rootScope.showLoading = false;

                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {

                        if (response.obj == "No Record Found") {
                            $scope.ShowLabel = true;
                            $scope.showTimeTable = false;
                        } else {
                            $scope.showTimeTable = false;
                            $scope.ShowLabel = false;
                            $scope.PreExamtableParams = new NgTableParams({
                            }, {
                                dataset: response.obj
                            });
                            $scope.preExamData = response.obj;
                            $scope.exportData();
                        }
                        
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    $scope.cancelSelection = function () {
        $scope.PreExam = {};
        $scope.showTimeTable = false;
        $scope.ShowLabel = false;
    };

    $scope.exportData = function () {
        
        alert("Please wait, Excel is being prepared...");

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "PreExamData_" + $scope.preExamData[0].ProgrammePartName + "_" +
            $scope.preExamData[0].BranchName + "_" + $scope.preExamData[0].ProgrammePartTermName + "_" +
            $scope.preExamData[0].ExamEvent + "_" + DateWithoutDashed + time;
        
        var mystyle = {
            headers: true,
            sheetid: 'PreExamDataInExcel',
            //style: 'font-size:15px;font-weight:bold;',
            caption: {
                title: '<h3 style=text-align:left;font-weight:bold> Event: ' + $scope.preExamData[0].ExamEvent +
                    '<br>Programme: ' + $scope.preExamData[0].ProgrammeName + 
                    '<br>Programme Part:' + $scope.preExamData[0].ProgrammePartName + 
                    '<br>Programme Part Term:' + $scope.preExamData[0].ProgrammePartTermName + '</h3>',                
            },
            
            columns: [
                
                { columnid: 'FacultyName', title: 'Faculty' },               
                { columnid: 'InstancePartTermName', title: 'Course Full Name' },
                { columnid: 'ProgrammeName', title: 'Course Name' },
                { columnid: 'ProgrammeCode', title: 'CourseABBR' },
                { columnid: 'ProgrammeModeName', title: 'Mode Of Learning' },
                { columnid: 'BranchName', title: 'Branch' },
                { columnid: 'ProgrammePartName', title: 'Course Part' },
                { columnid: 'ProgrammePartTermName', title: 'Course Term' },
                { columnid: 'PRN', title: 'PRN' },
                { columnid: 'NameAsPerMarksheet', title: 'Name AS printed ON STATEMENT OF marks' },
                { columnid: 'FirstName', title: 'First Name' },
                { columnid: 'MiddleName', title: 'Middle Name' },
                { columnid: 'LastName', title: 'Last Name' },
                { columnid: 'NameOfMother', title: 'Mother First Name' },
                { columnid: 'NameOfFather', title: 'Fathers FIRST Name' },
                { columnid: 'DOB', title: 'DATE OF Birth' },
                { columnid: 'InstructionMediumName', title: 'Medium OF Appearance' },
                { columnid: 'ApplicationReservationName', title: 'Category' },
                { columnid: 'MobileNo', title: 'Mobile Number' },
                { columnid: 'Gender', title: 'Gender' },
                { columnid: 'MaritalStatus', title: 'Marital Status' },
                { columnid: 'CurrentAddress', title: 'Correspondence Address' },
                { columnid: 'CurrentCityVillage', title: 'Correspondence City' },
                { columnid: 'CurrentPincode', title: 'Pin Code' },
                { columnid: 'StateName', title: 'STATE' },
                { columnid: 'DistrictName', title: 'District' },
                { columnid: 'OptionalMobileNo', title: 'ContactNumber' },
                { columnid: 'EmailId', title: 'EmailID' },
                { columnid: 'PhysicalDisability', title: 'Physical Disability' },
                { columnid: 'InstituteName', title: 'College/Study Center Name' },
                { columnid: 'PaperCode', title: 'PaperCode' },
                { columnid: 'PaperName', title: 'PaperName' },
                { columnid: 'TeachingLearningMethodName', title: 'Teaching Learning Method' },
                { columnid: 'AssessmentMethodName', title: 'Assessment Method' },
                { columnid: 'AssessmentType', title: 'Assessment TYPE' },
                { columnid: 'FormNo', title: 'Exam Form Number' },
                { columnid: 'AppearanceType', title: 'Exam Appearance TYPE' },
                { columnid: 'SeatNumber', title: 'Seat Number' },
                { columnid: 'CenterName', title: 'Center Name' },
                { columnid: 'VenueName', title: 'Venue Name' },
                { columnid: 'EligibilityByAcademics', title: 'Eligibility Status' },
                { columnid: 'InwardDate', title: 'Inward DATE' },
                { columnid: 'InwardTime', title: 'Inward TIME' },
                { columnid: 'ExamDate', title: 'Paper Exam DATE' },
                { columnid: 'Duration', title: 'Paper Exam TIME' },    
            ],

            /*rows: {
                //for putting background color in particular column
                0: {
                cell: {
                    style: 'font-size:17px;background:#115ea2;color:white;font-weight:bold'
                }
            },
        },*/
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.preExamData]);
    };
    
    
});