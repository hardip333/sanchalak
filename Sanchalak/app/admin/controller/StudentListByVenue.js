app.controller('StudentListByVenueCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams, $window) {

    $rootScope.pageTitle = "Manage Seat Number Generation";


    $rootScope.showLoading = false;

    $scope.DateWiseTable = false;

    $scope.cancelSeatNoGeneration = function () {
        $scope.examseat = {
            
            ProgrammeId:0,
            ExamMasterId: 0,
            FacultyExamMapId: 0,
            ProgrammePartTermId: 0,
            BranchId: 0,
            ExamVenueId:0

        };
        console.log($scope.examseat);
    };

    $scope.StudentListByVenue = {};

    $scope.getExamVenueList = function () {

        $http({
            method: 'POST',
            url: 'api/StudentListByVenue/ExamVenueGetByMstProgrammePartTermIdGet',
            data: { ProgrammePartTermId: $scope.examseat.ProgrammePartTermId },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                // $scope.Faculty = response.obj[0]; // Archana madam's code
                //debugger
                $scope.ExamVenueList = response.obj;
                // $scope.Faculty = response.obj; // Krunal's code               

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getStudentListByVenue = function () {
        //debugger
        $http({
            method: 'POST',
            url: 'api/StudentListByVenue/StudentListByVenueGet',
            data: {
                    ExamMasterId: $scope.examseat.ExamMasterId,
                    FacultyExamMapId: $scope.examseat.FacultyExamMapId,
                    ProgrammeId: $scope.examseat.ProgrammeId,
                    BranchId: $scope.examseat.BranchId,
                    ProgrammePartTermId: $scope.examseat.ProgrammePartTermId,
                    ExamVenueId: $scope.ExamVenueId
                  },
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
                    console.log(response.obj);
                    if (response.obj === "Record Not Found") {

                        $scope.NoRecordFound = true;
                        $scope.StudentListByVenueTableParams = new NgTableParams({
                        }, {
                            dataset: [],
                        });
                    }
                    else {
                        //debugger
                        $scope.ResLength = response.obj.length;
                        $scope.StudentListByVenueTableParams = new NgTableParams({
                            page: 1,
                            count: $scope.ResLength
                        },
                            { counts: [],dataset: response.obj });
                        $scope.DateWiseTable = true;
                        $scope.PaperList = response.obj;
                        $scope.ViewProgrammeName = $scope.PaperList[0].ProgrammeName;
                        $scope.ViewBranchName = $scope.PaperList[0].BranchName;
                        $scope.ViewExamVenueName = $scope.PaperList[0].ExamVenueName;
                        $scope.ViewExamVenueAddress = $scope.PaperList[0].ExamVenueAddress;
                        $scope.ViewExamEventName = $scope.PaperList[0].ExamEventName;
                        $scope.ViewStudentCount = $scope.PaperList[0].StudentCount;
                    }
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };   


    $scope.printDiv = function (divName, ReportName) {
        var printContents = document.getElementById(divName).innerHTML;
        //var style = "<style>";
        //style = style + "table {width: 1000px;font: 15px Calibri;}";
        //style = style + "table, th, td {border: solid 1px black; border-collapse: collapse;";
        //style = style + "padding: 2px 3px;}";
        //style = style + "</style>";

        // CREATE A WINDOW OBJECT.
        var win = window.open('', 'height=700,width=700');
        win.document.write('<html>');
        win.document.write('<head>');
        win.document.write('<title>Programme ' + ReportName + ' Report</title>');   // <title> FOR PDF HEADER.
        //win.document.write(style);          // ADD STYLE INSIDE THE HEAD TAG.
        win.document.write('<script src="scripts/angular/angular.min.js"></script>');
        win.document.write('<link rel="stylesheet" type="text/css" href="bower_components/bootstrap/css/bootstrap.min.css">');
        win.document.write('<script src="scripts/bootstrap-select/bootstrap-select.min.js"></script>');
        win.document.write('<script src="scripts/angular/ui-bootstrap-tpls-2.0.0.js"></script>');
        win.document.write('<script type="text/javascript" src="bower_components/bootstrap/js/bootstrap.min.js"></script>');
        win.document.write('<script type="text/javascript" src="bower_components/bootstrap-tagsinput/js/bootstrap-tagsinput.js"></script>');
        win.document.write('<script type="text/javascript" src="bower_components/bootstrap-maxlength/js/bootstrap-maxlength.js"></script>');
        win.document.write('</head>');
        win.document.write('<body>');
        win.document.write(printContents);         // THE TABLE CONTENTS INSIDE THE BODY TAG.
        win.document.write('</body></html>');
        win.document.close(); 	// CLOSE THE CURRENT WINDOW.
        setTimeout(function () { win.print(); }, 1000);            // PRINT THE CONTENTS.
        $scope.GAPR = {};
        $localStorage.reportData = {};
    };

    
    




   

  

});