import { TrainingTemplatePage } from './app.po';

describe('Training App', function() {
  let page: TrainingTemplatePage;

  beforeEach(() => {
    page = new TrainingTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
