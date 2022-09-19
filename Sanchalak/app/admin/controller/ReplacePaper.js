app.controller('ReplacePaperCtrl', function ($scope, $http, $rootScope, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {
    $rootScope.pageTitle = "Programme Part Term Detail";
   
    $scope.ReplacePaper = {};
    //$scope.PostProgInstPartTermTableparam = new NgTableParams(
    //    {}, {
    //    dataset: $scope.getProgInstList
    //});

    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }

    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }

    //Function for expand columns in row click
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

    $scope.resetReplacePaper = function () {
        $scope.ReplacePaper = {};
    };

    $scope.getAcademicList = function () {

        $http({
            method: 'POST',
            url: 'api/ReplacePaper/AcademicYearGetForDropDown',
            data: $scope.ReplacePaper,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.AcadList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getFacultyById = function () {
        //debugger;
        $http({
            method: 'POST',
            url: 'api/ReplacePaper/FacultyGetById',
            data: { Id: $cookies.get('InstituteId') },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                //debugger;
                // $scope.Faculty = response.obj[0]; // Archana madam's code
                $scope.FacultyList = response.obj;

                // $scope.Faculty = response.obj; // Krunal's code               



            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getProgrammeGetByFacId = function () {
        //debugger
        var PaperObj = { FacultyId: $scope.ReplacePaper.FacultyId }
        //alert(ProgObj);
        $http({
            method: 'POST',
            url: 'api/ReplacePaper/ProgrammeGetByFacId',
            data: PaperObj,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                // $scope.Faculty = response.obj[0]; // Archana madam's code
                //debugger
                $scope.ProgList = response.obj;
                // $scope.Faculty = response.obj; // Krunal's code               

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getSpecialisationGetByFacultyId = function (FacultyId, ProgrammeId) {
        //debugger
        $http({
            method: 'POST',
            url: 'api/ReplacePaper/SpecialisationGetByFacultyId',
            data: {
                FacultyId: $scope.ReplacePaper.FacultyId,
                ProgrammeId: $scope.ReplacePaper.ProgrammeId
            },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                // $scope.Faculty = response.obj[0]; // Archana madam's code
                //debugger
                $scope.SpecList = response.obj;
                // $scope.Faculty = response.obj; // Krunal's code               

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getProgrammeInstancePart = function (AcademicYearId,FacultyId, ProgrammeId,SpecialisationId) {
        //debugger
        $http({
            method: 'POST',
            url: 'api/ReplacePaper/IncProgrammeInstancePartGetByFIDPIDSID',
            data: {
                AcademicYearId: $scope.ReplacePaper.AcademicYearId,
                FacultyId: $scope.ReplacePaper.FacultyId,
                ProgrammeId: $scope.ReplacePaper.ProgrammeId,
                SpecialisationId: $scope.ReplacePaper.SpecialisationId,

            },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                // $scope.Faculty = response.obj[0]; // Archana madam's code
                //debugger
                $scope.ProgInstPartList = response.obj;
                // $scope.Faculty = response.obj; // Krunal's code               

            })
            .error(function (res) {
                alert(res);
            });
    };


    $scope.getProgrammeInstancePartTerm = function (AcademicYearId, FacultyId, ProgrammeId, SpecialisationId, ProgrammeInstancePartId) {
        //debugger
        $http({
            method: 'POST',
            url: 'api/ReplacePaper/IncProgInstPartTermGetByAIDFIDPIDSIDPIPID',
            data: {
                AcademicYearId: $scope.ReplacePaper.AcademicYearId,
                FacultyId: $scope.ReplacePaper.FacultyId,
                ProgrammeId: $scope.ReplacePaper.ProgrammeId,
                SpecialisationId: $scope.ReplacePaper.SpecialisationId,
                ProgrammeInstancePartId: $scope.ReplacePaper.ProgrammeInstancePartId

            },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                // $scope.Faculty = response.obj[0]; // Archana madam's code
                //debugger
                $scope.ProgInstPartTermList = response.obj;
                // $scope.Faculty = response.obj; // Krunal's code               
                
            })
            .error(function (res) {
                alert(res);
            });
    };
    
    $scope.getSourcePaperId = function (IncProgInstancePartTermId) {
        //debugger
        $http({
            method: 'POST',
            url: 'api/ReplacePaper/SourcePaperIdGet',
            data: { IncProgInstancePartTermId: $scope.ReplacePaper.IncProgInstancePartTermId },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                // $scope.Faculty = response.obj[0]; // Archana madam's code
                //debugger
                $scope.SourcePaperList = response.obj;
                // $scope.Faculty = response.obj; // Krunal's code 
                

            })
            .error(function (res) {
                alert(res);
            });
    };
    $scope.getDestinationPaperId = function (IncProgInstancePartTermId) {
        //debugger
        $http({
            method: 'POST',
            url: 'api/ReplacePaper/DestinationPaperIdGet',
            data: { IncProgInstancePartTermId: $scope.ReplacePaper.IncProgInstancePartTermId },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                // $scope.Faculty = response.obj[0]; // Archana madam's code
                //debugger
                $scope.DestinationPaperList = response.obj;
                // $scope.Faculty = response.obj; // Krunal's code 


            })
            .error(function (res) {
                alert(res);
            });
    };


   

});



