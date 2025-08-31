var ScormAPILibrary = {
    ScormInitialize: function() {
        if (!pipwerks.SCORM) {
            ScormHelper.ScormNotFoundError();
            return;
        }

        console.log("SCORM wrapper found. Initializing...");
        return pipwerks.SCORM.init();
    },

    ScormSetValue: function(element, value) {
        if (!pipwerks.SCORM) {
            ScormHelper.ScormNotFoundError();
            return;
        }

        var elementStr = UTF8ToString(element);
        var valueStr = UTF8ToString(value);
        console.log("SCORM SetValue:", elementStr, valueStr);
        pipwerks.SCORM.set(elementStr, valueStr);
    },

    ScormCommit: function() {
        if (!pipwerks.SCORM) {
            ScormHelper.ScormNotFoundError();
            return;
        }
        
        console.log("SCORM Commit");
        pipwerks.SCORM.save();
    },

    ScormFinish: function() {
        if (!pipwerks.SCORM) {
            ScormHelper.ScormNotFoundError();
            return;
        }
        
        console.log("SCORM Finish");
        pipwerks.SCORM.quit();
    }
};

mergeInto(LibraryManager.library, ScormAPILibrary);
