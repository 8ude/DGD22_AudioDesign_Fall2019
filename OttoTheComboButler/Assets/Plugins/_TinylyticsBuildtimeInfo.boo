//THIS SHOULD BE IN PLUGINS FOLDER
//DO NOT MOVE!!!



//NOTE: THIS COULD BREAK VERY EASILY
//Sort of a hack to get a build time. Relies on the fact that the Plugin folder is compiled first, and that Boo is compiled at buildtime. So, this
//is sort of dumb, but it means we can embed the time the build was made into an easily accessible class, and there isn't an easy way to do that otherwise.
//TODO: Find a better way to do this

macro tinylytics_build_buildtime_info():
    dateString = System.DateTime.Now.ToString()
    yield [|
        class Tinylytics_BuildtimeInfo:
            static def DateTimeString() as string:
                return "${$(dateString)}"
    |]
 
tinylytics_build_buildtime_info